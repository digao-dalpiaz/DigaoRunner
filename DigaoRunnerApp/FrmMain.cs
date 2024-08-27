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

            StVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Icon = Icon.ExtractAssociatedIcon(Environment.ProcessPath);

            ChangePage(false);

            BtnRun.Enabled = false;
            BtnCancel.Enabled = false;

            StAdmin.Visible = AdminRights.IsRunningAsAdministrator();
            StStatus.Text = null;

            ProgressBar.Visible = false;
            UpdateClock();

            LogService.Form = this;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_running)
            {
                Messages.Error("You can't close the app while script is running");
                e.Cancel = true;
                return;
            }

            WindowPlace.SaveWindowStateToRegistry(this, REG_KEY);
        }

        private void LoadReg()
        {
            using var key = Registry.CurrentUser.CreateSubKey(REG_KEY);

            EdLog.Font = new Font(
                (string)key.GetValue("FontName", LogService.DEFAULT_FONT),
                float.Parse((string)key.GetValue("FontSize", LogService.DEFAULT_SIZE.ToString())));

            LogService.Colors = new()
            {
                Normal = Color.FromArgb((int)key.GetValue("ColorNormal", LogService.DEFAULT_COLOR_NORMAL.ToArgb())),
                Error = Color.FromArgb((int)key.GetValue("ColorError", LogService.DEFAULT_COLOR_ERROR.ToArgb())),
            };

            EdLog.BackColor = Color.FromArgb((int)key.GetValue("ColorBack", LogService.DEFAULT_COLOR_BACK.ToArgb()));
        }

        private void SaveReg()
        {
            using var key = Registry.CurrentUser.CreateSubKey(REG_KEY);

            key.SetValue("FontName", EdLog.Font.Name);
            key.SetValue("FontSize", EdLog.Font.Size);

            key.SetValue("ColorNormal", LogService.Colors.Normal.ToArgb());
            key.SetValue("ColorError", LogService.Colors.Error.ToArgb());

            key.SetValue("ColorBack", EdLog.BackColor.ToArgb());
        }

        private void ChangePage(bool fieldsPage)
        {
            BoxFields.Visible = fieldsPage;
            EdLog.Visible = !fieldsPage;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            WindowPlace.LoadWindowStateFromRegistry(this, REG_KEY);
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

            LoadTitle();

            if (_fileContents.GetVar("ADMIN") == "true" && !StAdmin.Visible)
            {
                try
                {
                    AdminRights.RestartAsAdministrator(true);
                }
                catch (Exception ex)
                {
                    LogService.LogError("Error trying to execute as Admin: " + ex.Message);
                    LogService.SetStatus("Can't execute as Admin", StatusType.ERROR);
                }
                return;
            }

            new FieldsBuilder(_fileContents).BuildScreen();
            if (_fileContents.Fields.Count > 0)
            {
                LogService.SetStatus("Please fill initial parameters", StatusType.BELL);
                ChangePage(true);

                BtnRun.Enabled = true;
            }
            else
            {
                Run();
            }
        }

        private void LoadTitle()
        {
            string title = _fileContents.GetVar("TITLE");
            if (!string.IsNullOrEmpty(title)) this.Text = title;
        }

        private ResolvedFields ReadFieldsFromPanel()
        {
            ResolvedFields dic = [];

            foreach (var field in _fileContents.Fields)
            {
                dic.Add(field.Name, field.DefControlType.PropInfo.GetValue(field.Control));
            }

            return dic;
        }

        private void Run()
        {
            _running = true;
            LogService.SetStatus("Initializing...", StatusType.WAIT);

            BtnCancel.Enabled = true;

            _stopwatch.Start();
            UpdateClock();
            TimerControl.Enabled = true;

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
                catch (AbortException ex)
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

                    BtnCancel.Enabled = false;

                    TimerControl.Enabled = false;
                    _stopwatch.Stop();
                    UpdateClock();
                });
            });
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            BtnRun.Enabled = false;
            ChangePage(false);
            Run();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            BtnCancel.Enabled = false;
            _cancellationTokenSource.Cancel();
        }

        private void TimerControl_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            StElapsed.Text = "Elapsed: " + _stopwatch.Elapsed.ToString("d\\.hh\\:mm\\:ss");
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            var settings = new FrmConfig();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                SaveReg();
            }
        }

        private void StDigaoDalpiaz_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/digao-dalpiaz");
        }

        private void BtnRegisterExtension_Click(object sender, EventArgs e)
        {
            if (!StAdmin.Visible)
            {
                if (_running)
                {
                    Messages.Error("This requires restart de app with Admin elevation, but there is a process running now!");
                    return;
                }

                try
                {
                    AdminRights.RestartAsAdministrator(false);
                }
                catch (Exception ex)
                {
                    Messages.Error(ex.Message);
                }
                return;
            }

            AssociateExtension.Associate();
            MessageBox.Show("Script extension registered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
