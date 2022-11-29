using System;
using System.IO;
using SystemReadinessCore.Source.Libraries.ProcessManager;
using SystemReadinessCore.Source.Management.PrivilegesManager;

namespace SystemReadinessCore.Utilities.ModulesManager
{
    public partial class GetModules
    {
        public static void UninstallModules()
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
    }
}
