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

        public static void RestartAsAdministrator(bool withSameParameters)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Environment.ProcessPath,
                Arguments = withSameParameters ? string.Join(" ", Environment.GetCommandLineArgs().Skip(1).Select(x => $"\"{x}\"")) : null,
                Verb = "runas",
                UseShellExecute = true
            };

            Process.Start(startInfo);
            Application.Exit();
        }

    }
}
