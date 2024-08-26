namespace DigaoRunnerApp.Services
{
    public class LogService
    {

        public static FrmMain Form { get; set; }


        public static void SetStatus(string status, StatusType type)
        {
            Form.Invoke(() =>
            {
                Form.stStatus.Text = status;
                Form.stStatus.ForeColor = type == StatusType.OK ? Color.Green : type == StatusType.ERROR ? Color.Red : SystemColors.ControlText;
                Form.stStatus.Image = Form.images.Images[(int)type];
            });
        }

        public static void LogError(string text)
        {
            Log(text, Color.Crimson);
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

    public enum StatusType
    {
        WAIT = 0, OK = 1, ERROR = 2, BELL = 3
    }
}
