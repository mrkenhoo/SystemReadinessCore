using System;
using System.Diagnostics;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class ProcessManager
    {
        public static void NewProcess(string fileName, string? args)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = fileName;
                if (args != null) { process.StartInfo.Arguments = args; }
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.ErrorDialog = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                MessagesManager.ShowMessage(messageBoxText: ex.Message,
                                            caption: ex.Source,
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
    }
}
