using System;
using System.IO;
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
                try
                {
                    switch (File.Exists(RepositoryPath))
                    {
                        case true:
                            NewProcess.Run(FileName: "powershell.exe", Args: $"cd {RepositoryPath}\\{RepositoryName}; git pull" +
                                                                         $".\\Modules-Installer.ps1 -SourcePath {SourcePath}" +
                                                                         $" -DestinationPath {DestinationPath} -InstallationType Removal");
                            break;
                        case false:
                            throw new DirectoryNotFoundException();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
