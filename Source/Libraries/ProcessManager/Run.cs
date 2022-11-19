using System;
using System.Diagnostics;
using System.Windows;
using SystemReadinessCore.Libraries.MessagesManager;

namespace SystemReadinessCore.Libraries.ProcessManager
{
    public class NewProcess
    {
        public static int Run(string fileName, string? args)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = fileName;
                if (args != null) { process.StartInfo.Arguments = args; }
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.ErrorDialog = true;
                process.Start();
                process.WaitForExit();
                process.Close();
                return process.ExitCode;
            }
            catch (Exception ex)
            {
                NewMessage.Show(messageBoxText: ex.Message,
                                caption: ex.Source,
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
            return 0;
        }
    }
}
