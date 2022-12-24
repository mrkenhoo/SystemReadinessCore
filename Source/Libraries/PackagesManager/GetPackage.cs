using System;
using System.IO;
using SystemReadinessCore.Libraries.DependenciesManager;
using SystemReadinessCore.Libraries.ProcessManager;

namespace SystemReadinessCore.Libraries.PackagesManager
{
    public static class GetPackage
    {
        public static int Install(string packageName, string source)
        {
            if (packageName == null)
            {
                throw new ArgumentNullException(nameof(packageName));
            }
            else if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                switch (GetDependencies.IsWingetInstalled())
                {
                    case true:
                        NewProcess.Run(FileName: "winget.exe", Args: $"install --exact --id {packageName} --source {source}" +
                                                                  " --accept-source-agreements --accept-package-agreements");
                        return Environment.ExitCode;
                    case false:
                        throw new FileNotFoundException(message: "winget is not installed", fileName: "winget.exe");
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
    }
}
