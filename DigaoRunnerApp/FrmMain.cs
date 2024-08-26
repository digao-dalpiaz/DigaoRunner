using DigaoRunnerApp.Exceptions;
using DigaoRunnerApp.ScriptContext;
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
        private Fields _fields;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private readonly Stopwatch _stopwatch = new();

        public FrmMain()
        {
            InitializeComponent();

            stVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName);

            ChangePage(false);

            btnRun.Enabled = false;
            btnCancel.Enabled = false;

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
            var key = Registry.CurrentUser.CreateSubKey(REG_KEY);
            edLog.Font = new Font(
                (string)key.GetValue("FontName", edLog.Font.Name),
                float.Parse((string)key.GetValue("FontSize", edLog.Font.Size)));
        }

        private void SaveReg()
        {
            var key = Registry.CurrentUser.CreateSubKey(REG_KEY);

            key.SetValue("FontName", edLog.Font.Name);
            key.SetValue("FontSize", edLog.Font.Size);
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
                _fileContents = new ScriptLoader().LoadFile();
            }
            catch (ValidationException ex)
            {
                LogService.Log("ERROR LOADING SCRIPT: " + ex.Message, Color.Crimson);
                LogService.SetStatus("Script validation error", StatusType.ERROR);
                return;
            }

            _fields = new FieldsBuilder(_fileContents).Build();
            if (_fields.Count > 0) 
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

        private void Run()
        {
            _running = true;
            LogService.SetStatus("Initializing...", StatusType.WAIT);

            _stopwatch.Start();
            UpdateClock();
            timerControl.Enabled = true;

            var resolvedFields = _fields.ReadFieldsFromPanel();

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
                if (logError != null) LogService.Log(logError, Color.Crimson);

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

        private void btnFont_Click(object sender, EventArgs e)
        {
            var f = new FontDialog();
            f.Font = edLog.Font;
            if (f.ShowDialog() == DialogResult.OK)
            {
                edLog.Font = f.Font;

                SaveReg();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            btnRun.Enabled = false;
            ChangePage(false);
            Run();
        }
    }
}
