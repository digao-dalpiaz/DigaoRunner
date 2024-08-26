using DigaoRunnerApp.Exceptions;
using DigaoRunnerApp.Model;
using DigaoRunnerApp.Services;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;

namespace DigaoRunnerApp
{
    public partial class FrmMain : Form
    {

        private const string REG_KEY = @"SOFTWARE\DigaoRunner";

        private bool _running;
        private FileContents _fileContents;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private readonly Stopwatch _stopwatch = new();

        public FrmMain()
        {
            InitializeComponent();

            stVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Icon = Icon.ExtractAssociatedIcon(Environment.ProcessPath);

            ChangePage(false);

            btnRun.Enabled = false;
            btnCancel.Enabled = false;

            stAdmin.Visible = AdminRights.IsRunningAsAdministrator();
            stStatus.Text = null;

            progressBar.Visible = false;
            UpdateClock();

            LogService.Form = this;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_running)
            {
                MessageBox.Show("You can't close the app while script is running", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void LoadReg()
        {
            using var key = Registry.CurrentUser.CreateSubKey(REG_KEY);

            edLog.Font = new Font(
                (string)key.GetValue("FontName", LogService.DEFAULT_FONT),
                float.Parse((string)key.GetValue("FontSize", LogService.DEFAULT_SIZE)));

            LogService.Colors = new()
            {
                Normal = Color.FromArgb((int)key.GetValue("ColorNormal", LogService.DEFAULT_COLOR_NORMAL.ToArgb())),
                Error = Color.FromArgb((int)key.GetValue("ColorError", LogService.DEFAULT_COLOR_ERROR.ToArgb())),
            };

            edLog.BackColor = Color.FromArgb((int)key.GetValue("ColorBack", LogService.DEFAULT_COLOR_BACK.ToArgb()));
        }

        private void SaveReg()
        {
            using var key = Registry.CurrentUser.CreateSubKey(REG_KEY);

            key.SetValue("FontName", edLog.Font.Name);
            key.SetValue("FontSize", edLog.Font.Size);

            key.SetValue("ColorNormal", LogService.Colors.Normal.ToArgb());
            key.SetValue("ColorError", LogService.Colors.Error.ToArgb());

            key.SetValue("ColorBack", edLog.BackColor.ToArgb());
        }

        private void ChangePage(bool fieldsPage)
        {
            boxFields.Visible = fieldsPage;
            edLog.Visible = !fieldsPage;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadReg();

            try
            {
                _fileContents = ScriptLoader.LoadFile();
            }
            catch (ValidationException ex)
            {
                LogService.LogError("ERROR LOADING SCRIPT: " + ex.Message);
                LogService.SetStatus("Script validation error", StatusType.ERROR);
                return;
            }

            string title = _fileContents.GetVar("TITLE");
            if (!string.IsNullOrEmpty(title)) this.Text = title;

            if (_fileContents.GetVar("ADMIN") == "true" && !stAdmin.Visible)
            {
                try
                {
                    AdminRights.RestartAsAdministrator();
                }
                catch (Exception ex)
                {
                    LogService.LogError("Error trying to execute as Admin: " + ex.Message);
                    LogService.SetStatus("Can't execute as Admin", StatusType.ERROR);
                    return;
                }
                Application.Exit();
                return;
            }

            new FieldsBuilder(_fileContents).BuildScreen();
            if (_fileContents.Fields.Count > 0)
            {
                LogService.SetStatus("Please fill initial parameters", StatusType.BELL);
                ChangePage(true);

                btnRun.Enabled = true;
            }
            else
            {
                Run();
            }
        }

        private ResolvedFields ReadFieldsFromPanel()
        {
            ResolvedFields dic = [];

            foreach (var field in _fileContents.Fields)
            {
                var prop = field.Control.GetType().GetProperty(field.ValueProp);
                dic.Add(field.Name, prop.GetValue(field.Control));
            }

            return dic;
        }

        private void Run()
        {
            _running = true;
            LogService.SetStatus("Initializing...", StatusType.WAIT);

            btnCancel.Enabled = true;

            _stopwatch.Start();
            UpdateClock();
            timerControl.Enabled = true;

            var resolvedFields = ReadFieldsFromPanel();

            Task.Run(() =>
            {
                string status;
                string logError = null;
                bool completed = false;

                try
                {
                    new Engine(_fileContents, resolvedFields, _cancellationTokenSource).RunScript();

                    status = "Successfully completed!";
                    completed = true;
                }
                catch (ScriptFunctions.AbortException ex)
                {
                    logError = ex.Message;
                    status = "Script aborted";
                }
                catch (CancelException ex)
                {
                    logError = ex.Message;
                    status = "Script cancelled";
                }
                catch (CodeException ex)
                {
                    logError = "CODE ERROR: " + ex.Message;
                    status = "Script code error";
                }
                catch (Exception ex)
                {
                    logError = "FATAL ERROR: " + ex.Message;
                    status = "Fatal error";
                }

                LogService.SetStatus(status, completed ? StatusType.OK : StatusType.ERROR);
                if (logError != null) LogService.LogError(logError);

                Invoke(() =>
                {
                    _running = false;

                    btnCancel.Enabled = false;

                    timerControl.Enabled = false;
                    _stopwatch.Stop();
                    UpdateClock();
                });
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            _cancellationTokenSource.Cancel();
        }

        private void timerControl_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            stElapsed.Text = "Elapsed: " + _stopwatch.Elapsed.ToString("d\\.hh\\:mm\\:ss");
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            var settings = new FrmConfig();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                SaveReg();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            btnRun.Enabled = false;
            ChangePage(false);
            Run();
        }

        private void stDigaoDalpiaz_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/digao-dalpiaz");
        }
    }
}
