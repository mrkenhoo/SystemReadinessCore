using System;
using System.Diagnostics;
using System.Windows;
using SystemReadinessCore.Libraries.MessagesManager;

namespace SystemReadinessCore.Libraries.ProcessManager
{
    public class NewProcess
    {
        public static int Run(string fileName, string? args, bool RunAsAdministrator = false)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = fileName;
                if (args != null) { process.StartInfo.Arguments = args; }
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.ErrorDialog = true;
                if (RunAsAdministrator) { process.StartInfo.Verb = "runas"; }
                process.Start();
                process.WaitForExit();
                process.Close();
                process.Dispose();
            }
            catch (Exception ex)
            {
                NewMessage.Show(messageBoxText: ex.Message,
                                caption: ex.Source,
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
            return Environment.ExitCode;
        }
    }
}
