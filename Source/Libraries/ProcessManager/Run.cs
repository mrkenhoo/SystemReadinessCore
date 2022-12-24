using System;
using System.ComponentModel;
using System.Diagnostics;

namespace SystemReadinessCore.Libraries.ProcessManager
{
    public class NewProcess
    {
        public static void Run(string FileName, string? Args, bool RunAsAdministrator = false)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = FileName;
                if (Args != null) { process.StartInfo.Arguments = Args; }
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.ErrorDialog = true;

                switch (RunAsAdministrator)
                {
                    case true:
                        process.StartInfo.Verb = "runas";
                        break;
                    default:
                        break;
                }

                process.Start();
                process.WaitForExit();
                process.Dispose();
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Win32Exception)
            {
                throw;
            }
            catch (PlatformNotSupportedException)
            {
                throw;
            }
        }
    }
}
