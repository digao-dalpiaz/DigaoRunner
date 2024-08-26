using Microsoft.Win32;

namespace DigaoRunnerApp.Services
{
    public class AssociateExtension
    {

        public static void Associate()
        {
            const string EXT = ".drs";
            const string CMD = "drs_auto_file";

            using var regMain = Registry.ClassesRoot.CreateSubKey(EXT);
            regMain.SetValue("", CMD);

            using var regCmd = Registry.ClassesRoot.CreateSubKey(CMD + @"\shell\open\command");
            regCmd.SetValue("", $"\"{Environment.ProcessPath}\" \"%1\"");
        }

    }
}
