namespace DigaoRunnerApp.Services
{
    public class Messages
    {

        public static void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
