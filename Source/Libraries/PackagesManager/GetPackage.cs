using System;
using System.IO;
using SystemReadinessCore.Libraries.DependenciesManager;
using SystemReadinessCore.Libraries.ProcessManager;
using SystemReadinessCore.Management.PrivilegesManager;

namespace SystemReadinessCore.Libraries.PackagesManager
{
    public static class GetPackage
    {
        public static int Install(string packageName, string source)
        {
            if (GetPrivileges.IsUserAdmin())
            {
                try
                {
                    switch (GetDependencies.IsWingetInstalled())
                    {
                        case true:
                            NewProcess.Run(FileName: "winget.exe", Args: $"install --exact --id {packageName} --source {source}" +
                                                                      " --accept-source-agreements --accept-package-agreements");
                            return Environment.ExitCode;
                        case false:
                            return Environment.ExitCode;
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
        public static int Uninstall(string packageName)
        {
            if (GetPrivileges.IsUserAdmin())
            {
                try
                {
                    switch (GetDependencies.IsWingetInstalled())
                    {
                        case true:
                            NewProcess.Run(FileName: "winget.exe", Args: $"uninstall --exact --id {packageName} --accept-source-agreements");
                            return Environment.ExitCode;
                        case false:
                            throw new FileNotFoundException();
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
