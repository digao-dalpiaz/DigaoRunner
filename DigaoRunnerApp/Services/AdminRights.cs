using System.Diagnostics;
using System.Security.Principal;

namespace DigaoRunnerApp.Services
{
    public class AdminRights
    {
        public static bool IsRunningAsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RestartAsAdministrator()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Environment.ProcessPath,
                Arguments = string.Join(" ", Environment.GetCommandLineArgs().Skip(1)),
                Verb = "runas",
                UseShellExecute = true
            };

            Process.Start(startInfo);
        }

    }
}
