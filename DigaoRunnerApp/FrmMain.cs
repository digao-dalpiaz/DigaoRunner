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

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private readonly Stopwatch _stopwatch = new();

        public FrmMain()
        {
            InitializeComponent();

            stVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName);

            stStatus.Text = null;
            stElapsed.Text = null;
            progressBar.Visible = false;

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

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadReg();

            _running = true;
            stStatus.Text = "Initializing...";
            stStatus.Image = images.Images[0];

            _stopwatch.Start();
            UpdateClock();
            timerControl.Enabled = true;

            Task.Run(() =>
            {
                string status;
                string logError = null;
                bool completed = false;

                try
                {
                    var contents = new ScriptLoader().LoadFile();
                    new Engine(contents, _cancellationTokenSource).RunScript();

                    status = "Successfully completed!";
                    completed = true;
                }
                catch (ValidationException ex)
                {
                    logError = ex.Message;
                    status = "Script validation error";
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

                if (logError != null) LogService.Log(logError, Color.Crimson);
                
                Invoke(() =>
                {
                    _running = false;
                    stStatus.Text = status;
                    stStatus.ForeColor = completed ? Color.Green : Color.Red;
                    stStatus.Image = images.Images[completed ? 1 : 2];

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

    }
}
