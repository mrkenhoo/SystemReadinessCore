using System;
using System.Windows;
using SystemReadinessCore.Libraries.MessagesManager;
using SystemReadinessCore.Libraries.ProcessManager;
using SystemReadinessCore.Management.PrivilegesManager;
using SystemReadinessCore.Source.Libraries.DependenciesManager;

namespace SystemReadinessCore.Libraries.PackagesManager
{
    public static class GetPackage
    {
        public static void Install(string packageName, string source)
        {
            if (!GetPrivileges.IsUserAdmin())
            {
                if (!Find.IsWingetInstalled())
                {
                    NewMessage.Show(messageBoxText: $"Cannot install {packageName} because winget is either not installed or outdated.",
                                                caption: "Error",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        NewProcess.Run(fileName: "winget.exe",
                                                  args: $"install --exact --id {packageName} --source {source}" +
                                                    " --accept-source-agreements --accept-package-agreements");
                    }
                    catch (Exception ex)
                    {
                        NewMessage.Show(messageBoxText: ex.Message,
                                                    caption: ex.Source,
                                                    button: MessageBoxButton.OK,
                                                    icon: MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                NewMessage.Show(messageBoxText: $"Before installing {packageName}, make sure you are not running this program as administrator.",
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
        public static void Uninstall(string packageName, bool silent = false)
        {
            if (!GetPrivileges.IsUserAdmin())
            {
                if (!Find.IsWingetInstalled())
                {
                    NewMessage.Show(messageBoxText: $"Cannot uninstall {packageName} because winget is either not installed or outdated.",
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
                            NewProcess.Run(fileName: "winget.exe",
                                                      args: $"uninstall --exact --id {packageName} --accept-source-agreements");
                        }
                        else
                        {
                            NewProcess.Run(fileName: "winget.exe",
                                                      args: $"uninstall --exact --id {packageName} --accept-source-agreements --silent");
                        }
                    }
                    catch (Exception ex)
                    {
                        NewMessage.Show(messageBoxText: ex.Message,
                                                    caption: ex.Source,
                                                    button: MessageBoxButton.OK,
                                                    icon: MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                NewMessage.Show(messageBoxText: $"Cannot uninstall {packageName} as administrator.",
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
    }
}
