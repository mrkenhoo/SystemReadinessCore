using System;
using System.IO;
using System.Windows;
using SystemReadinessCore.Libraries.MessagesManager;
using SystemReadinessCore.Libraries.ProcessManager;
using SystemReadinessCore.Management.PrivilegesManager;

namespace SystemReadinessCore.Utilities.ModulesManager
{
    public partial class GetModules
    {
        public static void UninstallModules()
        {
            if (GetPrivileges.IsUserAdmin())
            {
                if (Directory.Exists($"{RepositoryPath}"))
                {
                    try
                    {
                        NewProcess.Run(fileName: "powershell.exe", args: $"cd {RepositoryPath};" +
                                                                         $" .\\Modules-Installer.ps1 -SourcePath {SourcePath}" +
                                                                         $" -DestinationPath {DestinationPath} -InstallationType Removal");
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
                NewMessage.Show(messageBoxText: "Cannot uninstall PowerShell modules without administrator privileges.",
                                caption: "Error",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
        }
    }
}