namespace DigaoRunnerApp.Services
{
    public class LogService
    {

        public static FrmMain Form { get; set; }

        public static void SetTitle(string title)
        {
            Form.Invoke(() => 
            {
                Form.Text = title;
            });
        }

        public static void SetStatus(string status)
        {
            Form.Invoke(() =>
            {
                Form.stStatus.Text = status;
            });
        }

        public static void Log(string text, Color color)
        {
            Form.Invoke(() =>
            {
                var ed = Form.edLog;

                ed.SelectionStart = ed.TextLength;
                ed.SelectionColor = color;
                ed.SelectedText = text + Environment.NewLine;
            });
        }

        public static void SwitchProgress(bool enable)
        {
            Form.Invoke(() =>
            {
                Form.progressBar.Visible = enable;
            });
        }

        public static void Progress(long position, long total)
        {
            Form.Invoke(() =>
            {
                Form.progressBar.Value = (int)Math.Floor((double)position * 100 / total);
            });
        }

    }

    public enum LogType
    {
        NORMAL,
        ERROR
    }
}
