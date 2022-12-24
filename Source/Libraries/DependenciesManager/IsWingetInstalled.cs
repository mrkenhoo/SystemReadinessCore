using System;
using System.IO;

namespace SystemReadinessCore.Libraries.DependenciesManager
{
    public static class GetDependencies
    {
        private static readonly string WingetExecutable =
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                    "\\Microsoft\\WindowsApps\\winget.exe";

        public static bool IsWingetInstalled()
        {
            switch (File.Exists(WingetExecutable))
            {
                case true:
                    return true;
                case false:
                    return false;
            }
        }
    }
}
