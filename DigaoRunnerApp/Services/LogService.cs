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
                    StatusType.OK => Color.Lime,
                    StatusType.ERROR => Color.Red,
                    StatusType.WAIT => Color.SteelBlue,
                    StatusType.BELL => Color.Gold,
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
                    case "PROC_N":
                        color = Customization.Instance.ColorProcNormal;
                        break;
                    case "PROC_E":
                        color = Customization.Instance.ColorProcError;
                        break;
                }

                //

                BeginUpdate(ed);

                Point pt = new();
                SendMessage(ed.Handle, EM_GETSCROLLPOS, IntPtr.Zero, ref pt);

                var currentSelStart = ed.SelectionStart;
                var currentSelLength = ed.SelectionLength;
                bool wasAtEnd = (currentSelStart == ed.TextLength);

                ed.SelectionStart = ed.TextLength;

                ed.SelectionColor = color;
                ed.SelectedText = text + Environment.NewLine;

                if (wasAtEnd)
                {
                    ed.ScrollToCaret();
                }
                else
                {
                    ed.Select(currentSelStart, currentSelLength);
                    SendMessage(ed.Handle, EM_SETSCROLLPOS, IntPtr.Zero, ref pt);
                }

                EndUpdate(ed);
            });
        }

        //--
        private const int EM_GETSCROLLPOS = 0x0400 + 221;
        private const int EM_SETSCROLLPOS = 0x0400 + 222;

        private const int WM_SETREDRAW = 0x000B;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref Point lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private static void BeginUpdate(RichTextBox richTextBox)
        {
            SendMessage(richTextBox.Handle, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
        }

        private static void EndUpdate(RichTextBox richTextBox)
        {
            SendMessage(richTextBox.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            richTextBox.Invalidate();
        }
        //--

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
