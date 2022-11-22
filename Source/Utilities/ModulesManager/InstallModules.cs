using System;
using System.IO;
using System.Windows;
using SystemReadinessCore.Libraries.MessagesManager;
using SystemReadinessCore.Libraries.ProcessManager;
using SystemReadinessCore.Management.PrivilegesManager;
using static SystemReadinessCore.Libraries.RuntimeManager.Runtime;

namespace SystemReadinessCore.Utilities.ModulesManager
{
    public partial class GetModules
    {
        private static string RepositoryName => "PowerShell-Modules";
        private static string RepositoryUrl => "https://github.com/mrkenhoo/PowerShell-Modules.git";
        private static string RepositoryPath =>
            $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{GetRuntimeInfo.AssemblyTitle}";
        private static string SourcePath => $".\\CustomModules";
        private static string DestinationPath =>
            $"{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}\\System32\\WindowsPowerShell\\v1.0\\Modules";

        public static void InstallModules()
        {
            if (GetPrivileges.IsUserAdmin())
            {
                try
                {
                    if (!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Git\\bin\\git.exe"))
                    {
                        NewMessage.Show(messageBoxText: "Cannot install PowerShell modules, git was not found.",
                                        caption: "Error",
                                        button: MessageBoxButton.OK,
                                        icon: MessageBoxImage.Error);
                    }
                    if (!Directory.Exists(RepositoryPath))
                    {
                        Directory.CreateDirectory(RepositoryPath);
                        NewProcess.Run(fileName: "powershell.exe", args: $"git clone {RepositoryUrl} {RepositoryPath}\\{RepositoryName}" +
                                                                         $"cd {RepositoryPath}\\{RepositoryName};" +
                                                                         $".\\Modules-Installer.ps1 -SourcePath {SourcePath}" +
                                                                         $" -DestinationPath {DestinationPath} -InstallationType Deploy");
                    }
                    else
                    {
                        NewProcess.Run(fileName: "powershell.exe", args: $"cd {RepositoryPath}\\{RepositoryName}; git pull" +
                                                                         $".\\Modules-Installer.ps1 -SourcePath {SourcePath}" +
                                                                         $" -DestinationPath {DestinationPath} -InstallationType Deploy");
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
            else
            {
                NewMessage.Show(messageBoxText: "Cannot install PowerShell modules without administrator privileges.",
                                caption: "Error",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
        }
    }
}
