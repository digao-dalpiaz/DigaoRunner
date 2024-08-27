using System.Runtime.InteropServices;

namespace DigaoRunnerApp.Services
{
    public class WindowPlace
    {

        public static void LoadWindowStateFromRegistry(Form f, string key)
        {
            var r = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(key + @"\Wnd");

            if ((int)r.GetValue("Stored", 0) != 1) return;

            var wpTemp = GetPlacement();
            wpTemp.rcNormalPosition = new System.Drawing.Rectangle(
                (int)r.GetValue("X"),
                (int)r.GetValue("Y"),
                (int)r.GetValue("W"),
                (int)r.GetValue("H"));

            if ((int)r.GetValue("Max") == 1)
            {
                wpTemp.showCmd = ShowWindowCommands.Maximized;
            }

            SetWindowPlacement(f.Handle, ref wpTemp);
        }

        public static void SaveWindowStateToRegistry(Form f, string key)
        {
            var r = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(key + @"\Wnd");

            var wpTemp = GetPlacement();
            GetWindowPlacement(f.Handle, ref wpTemp);

            r.SetValue("X", wpTemp.rcNormalPosition.Left);
            r.SetValue("Y", wpTemp.rcNormalPosition.Top);

            r.SetValue("W", wpTemp.rcNormalPosition.Right - wpTemp.rcNormalPosition.Left);
            r.SetValue("H", wpTemp.rcNormalPosition.Bottom - wpTemp.rcNormalPosition.Top);

            r.SetValue("Max", wpTemp.showCmd == ShowWindowCommands.Maximized ? 1 : 0);
            r.SetValue("Stored", 1);
        }

        private static WINDOWPLACEMENT GetPlacement()
        {
            WINDOWPLACEMENT placement = new();
            placement.length = Marshal.SizeOf(placement);
            return placement;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowCommands showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        internal enum ShowWindowCommands : int
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }

    }
}
