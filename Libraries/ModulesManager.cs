using System;
using System.IO;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class ModulesManager
    {
        public static void InstallModules()
        {
            if (PrivilegesManager.IsUserAdmin())
            {
                if (!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Git\\bin\\git.exe"))
                {
                    MessagesManager.ShowMessage(messageBoxText: "Please install Git for installing PowerShell modules.",
                                                caption: "Error",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);
                }
                string repositoryUrl = "https://github.com/mrkenhoo/PowerShell-Modules.git";
                string repositoryPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\sunvalley-srw";
                string sourcePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\sunvalley-srw\\PowerShell-Modules\\CustomModules";
                string destinationPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.System)}\\WindowsPowerShell\\v1.0\\Modules";
                if (!Directory.Exists(repositoryPath))
                {
                    Directory.CreateDirectory(repositoryPath);
                }
                if (!Directory.Exists($"{repositoryPath}\\PowerShell-Modules"))
                {
                    ProcessManager.NewProcess(fileName: "powershell.exe", args: $"git clone {repositoryUrl} {repositoryPath}\\PowerShell-Modules");
                }
                else
                {
                    ProcessManager.NewProcess(fileName: "powershell.exe", args: $"cd {repositoryPath}\\PowerShell-Modules; git pull");
                }
                ProcessManager.NewProcess(fileName: "powershell.exe", args: $"cd {repositoryPath}\\PowerShell-Modules\\;" +
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
        public static void UninstallModules()
        {
            if (PrivilegesManager.IsUserAdmin())
            {
                string repositoryPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\sunvalley-srw";
                string sourcePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\sunvalley-srw\\PowerShell-Modules\\CustomModules";
                string destinationPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.System)}\\WindowsPowerShell\\v1.0\\Modules";
                if (Directory.Exists($"{repositoryPath}\\PowerShell-Modules"))
                {
                    try
                    {
                        ProcessManager.NewProcess(fileName: "powershell.exe", args: $"cd {repositoryPath}\\PowerShell-Modules\\;" +
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
