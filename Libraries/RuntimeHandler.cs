using System.Windows;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SystemReadinessCore.Libraries
{
    public class RuntimeHandler
    {
        public static bool IsAlreadyRunning()
        {
            bool isRunning = false;
            try
            {
                Process process = Process.GetCurrentProcess();
                Process[] processList = Process.GetProcessesByName(process.ProcessName);

                if (processList != null && processList.Any() && processList.Length > 1)
                {
                    MessagesManager.ShowMessage(messageBoxText: "This program is already running",
                                                caption: "Error",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);
                    isRunning = true;
                }
                else
                {
                    isRunning = false;
                }
            }
            catch (Exception e)
            {
                MessagesManager.ShowMessage(messageBoxText: e.Message,
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
            return isRunning;
        }

        public static bool IsSystemCompatible()
        {
            bool isCompatible;
            if (Environment.OSVersion.Version.Build < 19041)
            {
                isCompatible = false;
            }
            else
            {
                isCompatible = true;
            }
            return isCompatible;
        }
    }
}
