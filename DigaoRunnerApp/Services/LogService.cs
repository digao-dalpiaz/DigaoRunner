using DigaoRunnerApp.Properties;

namespace DigaoRunnerApp.Services
{
    public class LogService
    {

        public readonly static string DEFAULT_FONT = "Consolas";
        public readonly static float DEFAULT_SIZE = 12;
        public readonly static Color DEFAULT_COLOR_NORMAL = Color.LimeGreen;
        public readonly static Color DEFAULT_COLOR_ERROR = Color.Crimson;
        public readonly static Color DEFAULT_COLOR_BACK = Color.FromArgb(30, 30, 30);
        public readonly static bool DEFAULT_WORD_WRAP = true;

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
                Form.StStatus.Text = status;

                Form.StStatus.ForeColor = type switch
                {
                    StatusType.OK => Color.Green,
                    StatusType.ERROR => Color.Red,
                    StatusType.WAIT => Color.SteelBlue,
                    StatusType.BELL => Color.Black,
                    _ => throw new Exception("Invalid status type")
                };

                Form.StStatus.Image = type switch
                {
                    StatusType.OK => Resources.ok,
                    StatusType.ERROR => Resources.error,
                    StatusType.WAIT => Resources.wait,
                    StatusType.BELL => Resources.bell,
                    _ => throw new Exception("Invalid status type")
                };
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
                var ed = Form.EdLog;

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
                Form.ProgressBar.Visible = enable;
            });
        }

        public static void Progress(long position, long total)
        {
            Form.Invoke(() =>
            {
                Form.ProgressBar.Value = (int)Math.Floor((double)position * 100 / total);
            });
        }

    }

    public enum StatusType
    {
        WAIT = 0, OK = 1, ERROR = 2, BELL = 3
    }

}
