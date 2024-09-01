using Microsoft.Win32;

namespace DigaoRunnerApp.Services
{
    public class AssociateExtension
    {

        public static void Associate()
        {
            const string EXT = ".ds";
            const string CMD = "ds_auto_file";

            using var mainKey = Registry.ClassesRoot.CreateSubKey(EXT);
            mainKey.SetValue("", CMD);

            using var cmdKey = Registry.ClassesRoot.CreateSubKey(CMD);
            cmdKey.SetValue("", "Digao Script File");

            using var shellKey = cmdKey.CreateSubKey(@"shell\open\command");
            shellKey.SetValue("", $"\"{Environment.ProcessPath}\" \"%1\"");
        }

    }
}
