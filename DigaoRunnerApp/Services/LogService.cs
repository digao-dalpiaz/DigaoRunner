namespace DigaoRunnerApp.Services
{
    public class LogService
    {

        public static string DEFAULT_FONT = "Consolas";
        public static float DEFAULT_SIZE = 12;
        public static Color DEFAULT_COLOR_NORMAL = Color.LimeGreen;
        public static Color DEFAULT_COLOR_ERROR = Color.Crimson;
        public static Color DEFAULT_COLOR_BACK = Color.FromArgb(30, 30, 30);

        public static FrmMain Form { get; set; }

        public static DefColors Colors { get; set; }

        public class DefColors
        {
            public Color Normal;
            public Color Error;
        }

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
            Log(text, Color.Empty, "E");
        }

        public static void Log(string text, Color color, string type = null)
        {
            Form.Invoke(() =>
            {
                var ed = Form.edLog;

                switch (type)
                {
                    case "N":
                        color = Colors.Normal;
                        break;
                    case "E":
                        color = Colors.Error;
                        break;
                }

                ed.SelectionStart = ed.TextLength;
                ed.SelectionColor = color;
                ed.SelectedText = text + Environment.NewLine;

                ed.ScrollToCaret();
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
