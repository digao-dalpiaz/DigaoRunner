using DigaoRunnerApp.Properties;

namespace DigaoRunnerApp.Services
{
    public class LogService
    {

        public static FrmMain Form { get; set; }

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
                        color = Customization.Instance.ColorNormal;
                        break;
                    case "E":
                        color = Customization.Instance.ColorError;
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
