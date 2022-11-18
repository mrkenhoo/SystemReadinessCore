using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using SystemReadinessCore.Libraries.MessagesManager;

namespace SystemReadinessCore.Utilities.RuntimeHandler
{
    public partial class Runtime
    {
        public static bool IsAlreadyRunning()
        {
            try
            {
                Process process = Process.GetCurrentProcess();
                Process[] processList = Process.GetProcessesByName(process.ProcessName);

                if (processList != null && processList.Any() && processList.Length > 1)
                {
                    NewMessage.Show(messageBoxText: "This program is already running",
                                    caption: "Error",
                                    button: MessageBoxButton.OK,
                                    icon: MessageBoxImage.Error);
                    return true;
                }
            }
            catch (Exception ex)
            {
                NewMessage.Show(messageBoxText: ex.Message,
                                caption: ex.Source,
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
            return false;
        }
    }
}
