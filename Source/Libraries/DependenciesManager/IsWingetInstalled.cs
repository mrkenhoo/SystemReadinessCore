using System;
using System.IO;

namespace SystemReadinessCore.Source.Libraries.DependenciesManager
{
    public static class Find
    {
        public static bool IsWingetInstalled()
        {
            string LocalWindowsAppsDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\WindowsApps\\";
            bool ExecutableExists;
            if (File.Exists(LocalWindowsAppsDir + "winget.exe"))
            {
                ExecutableExists = true;
            }
            else
            {
                ExecutableExists = false;
            }
            return ExecutableExists;
        }
    }
}
