using System;
using SystemReadinessCore.Source.Libraries.DependenciesManager;
using SystemReadinessCore.Source.Libraries.ProcessManager;
using SystemReadinessCore.Source.Management.PrivilegesManager;

namespace SystemReadinessCore.Source.Libraries.PackagesManager
{
    public static class GetPackage
    {
        public static int Install(string packageName, string source)
        {
            try
            {
                if (!GetPrivileges.IsUserAdmin())
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
                else
                {
                    throw new AccessViolationException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Uninstall(string packageName)
        {
            try
            {
                if (!GetPrivileges.IsUserAdmin())
                {
                    switch (GetDependencies.IsWingetInstalled())
                    {
                        case true:
                            NewProcess.Run(FileName: "winget.exe", Args: $"uninstall --exact --id {packageName} --accept-source-agreements");
                            return Environment.ExitCode;
                        case false:
                            throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
