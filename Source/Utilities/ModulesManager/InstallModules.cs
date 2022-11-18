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
        static string RepositoryName => "PowerShell-Modules";
        static string RepositoryUrl => "https://github.com/mrkenhoo/PowerShell-Modules.git";
        static string RepositoryPath => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{RepositoryName}";
        static string SourcePath => $"{RepositoryPath}\\CustomModules";
        static string DestinationPath => $"{Environment.GetFolderPath(Environment.SpecialFolder.SystemX86)}\\WindowsPowerShell\\v1.0\\Modules";

        public static void InstallModules()
        {
            if (GetPrivileges.IsUserAdmin())
            {
                if (!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Git\\bin\\git.exe"))
                {
                    NewMessage.Show(messageBoxText: "Please install Git for installing PowerShell modules.",
                                    caption: "Error",
                                    button: MessageBoxButton.OK,
                                    icon: MessageBoxImage.Error);
                }
                if (!Directory.Exists(RepositoryPath))
                {
                    Directory.CreateDirectory(RepositoryPath);
                    NewProcess.Run(fileName: "powershell.exe", args: $"git clone {RepositoryUrl} {RepositoryPath}.git");
                }
                else
                {
                    NewProcess.Run(fileName: "powershell.exe", args: $"cd {RepositoryPath}; git pull");
                }
                NewProcess.Run(fileName: "powershell.exe", args: $"cd {RepositoryPath};" +
                                                                 $" .\\Modules-Installer.ps1 -SourcePath {SourcePath}" +
                                                                 $" -DestinationPath {DestinationPath} -InstallationType Deploy");
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
