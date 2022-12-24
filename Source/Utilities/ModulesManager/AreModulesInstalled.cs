using System;
using System.IO;

namespace SystemReadinessCore.Libraries.ModulesManager
{
    public partial class GetModules
    {
        private static readonly string[] SourcePathList = Directory.GetFiles(SourcePath);
        private static readonly string[] DestinationPathList = Directory.GetDirectories(DestinationPath);
        private static bool ModulesAreInstalled { get; set; }

        public static bool AreModulesInstalled()
        {
            try
            {
                foreach (string folder in DestinationPathList)
                {
                    if (!Directory.Exists($"{DestinationPathList}\\{SourcePathList}"))
                    {
                          ModulesAreInstalled = true;
                    }
                    else
                    {
                        ModulesAreInstalled = false;
                    }
                }

                return ModulesAreInstalled;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
