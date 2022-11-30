using System;
using System.IO;
using SystemReadinessCore.Libraries.ProcessManager;
using SystemReadinessCore.Management.PrivilegesManager;
using static SystemReadinessCore.Libraries.RuntimeManager.Runtime;

namespace SystemReadinessCore.Utilities.ModulesManager
{
    public partial class GetModules
    {
        private static readonly string RepositoryName = "PowerShell-Modules";
        private static readonly string RepositoryUrl = "https://github.com/mrkenhoo/PowerShell-Modules.git";
        private static readonly string RepositoryPath =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{GetRuntimeInfo.AssemblyTitle}";
        private static readonly string SourcePath = $".\\CustomModules";
        private static readonly string DestinationPath =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}\\System32\\WindowsPowerShell\\v1.0\\Modules";
        private static readonly string GitDirectory =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Git\\bin\\git.exe";

        public static void InstallModules()
        {
            if (GetPrivileges.IsUserAdmin())
            {
                try
                {
                    if (!File.Exists(GitDirectory)) { throw new FileNotFoundException(); }

                    switch (File.Exists(RepositoryPath))
                    {
                        case true:
                            NewProcess.Run(FileName: "powershell.exe", Args: $"cd {RepositoryPath}\\{RepositoryName}; git pull" +
                                                                         $".\\Modules-Installer.ps1 -SourcePath {SourcePath}" +
                                                                         $" -DestinationPath {DestinationPath} -InstallationType Deploy");
                            break;
                        case false:
                            Directory.CreateDirectory(RepositoryPath);
                            NewProcess.Run(FileName: "powershell.exe", Args: $"git clone {RepositoryUrl} {RepositoryPath}\\{RepositoryName}" +
                                                                             $"cd {RepositoryPath}\\{RepositoryName};" +
                                                                             $".\\Modules-Installer.ps1 -SourcePath {SourcePath}" +
                                                                             $" -DestinationPath {DestinationPath} -InstallationType Deploy");
                            break;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new AccessViolationException();
            }
        }
    }
}
