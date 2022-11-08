using System;
using System.IO;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class ModulesManager
    {
        public static void InstallModules(string repoName)
        {
            string repositoryUrl = "https://github.com/mrkenhoo/PowerShell-Modules.git";
            string repositoryPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{repoName}";
            string sourcePath = $"{repositoryPath}\\{repoName}";
            string destinationPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.System)}\\WindowsPowerShell\\v1.0\\Modules";

            if (PrivilegesManager.IsUserAdmin())
            {
                if (!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Git\\bin\\git.exe"))
                {
                    MessagesManager.ShowMessage(messageBoxText: "Please install Git for installing PowerShell modules.",
                                                caption: "Error",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);
                }
                if (!Directory.Exists(repositoryPath))
                {
                    Directory.CreateDirectory(repositoryPath);
                    ProcessManager.NewProcess(fileName: "powershell.exe", args: $"git clone {repositoryUrl} {repositoryPath}.git");
                }
                else
                {
                    ProcessManager.NewProcess(fileName: "powershell.exe", args: $"cd {repositoryPath}; git pull");
                }
                ProcessManager.NewProcess(fileName: "powershell.exe", args: $"cd {repositoryPath};" +
                                                                             $" .\\Modules-Installer.ps1 -SourcePath {sourcePath}" +
                                                                             $" -DestinationPath {destinationPath} -InstallationType Deploy");
            }
            else
            {
                MessagesManager.ShowMessage(messageBoxText: "Cannot install PowerShell modules without administrator privileges.",
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
        public static void UninstallModules(string repoName)
        {
            string repositoryPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{repoName}";
            string sourcePath = $"{repositoryPath}\\{repoName}";
            string destinationPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.System)}\\WindowsPowerShell\\v1.0\\Modules";

            if (PrivilegesManager.IsUserAdmin())
            {
                if (Directory.Exists($"{repositoryPath}"))
                {
                    try
                    {
                        ProcessManager.NewProcess(fileName: "powershell.exe", args: $"cd {repositoryPath};" +
                                                                             $" .\\Modules-Installer.ps1 -SourcePath {sourcePath}" +
                                                                             $" -DestinationPath {destinationPath} -InstallationType Removal");
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
                MessagesManager.ShowMessage(messageBoxText: "Cannot uninstall PowerShell modules without administrator privileges.",
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
    }
}
