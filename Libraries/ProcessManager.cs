using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class ProcessManager
    {
        protected static int ExitCode => Environment.ExitCode;

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
                if (ExitCode == 1)
                {
                    MessagesManager.ShowMessage(messageBoxText: "Something went wrong.",
                                                caption: "Error",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessagesManager.ShowMessage(messageBoxText: ex.Message,
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
    }
}
