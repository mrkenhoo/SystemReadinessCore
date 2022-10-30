using System;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class PackagesManager
    {
        public static void InstallPackage(string packageName, string source)
        {
            if (!PrivilegesManager.IsUserAdmin())
            {
                if (!DependenciesManager.IsWingetInstalled())
                {
                    MessagesManager.ShowMessage(messageBoxText: $"Cannot install {packageName} because winget is either not installed or outdated.",
                                                caption: "Error",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        ProcessManager.NewProcess(fileName: "winget.exe",
                                                  args: $"install --exact --id {packageName} --source {source}" +
                                                    " --accept-source-agreements --accept-package-agreements");
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
            else
            {
                MessagesManager.ShowMessage(messageBoxText: $"Before installing {packageName}, make sure you are not running this program as administrator.",
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
        public static void RemovePackage(string packageName, bool silent = false)
        {
            if (!PrivilegesManager.IsUserAdmin())
            {
                if (!DependenciesManager.IsWingetInstalled())
                {
                    MessagesManager.ShowMessage(messageBoxText: $"Cannot uninstall {packageName} because winget is either not installed or outdated.",
                                                caption: "Error",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        if (!silent)
                        {
                            ProcessManager.NewProcess(fileName: "winget.exe",
                                                      args: $"uninstall --exact --id {packageName} --accept-source-agreements");
                        }
                        else
                        {
                            ProcessManager.NewProcess(fileName: "winget.exe",
                                                      args: $"uninstall --exact --id {packageName} --accept-source-agreements --silent");
                        }
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
            else
            {
                MessagesManager.ShowMessage(messageBoxText: $"Cannot install {packageName} as administrator.",
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
    }
}
