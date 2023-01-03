using System;
using System.ComponentModel;
using System.Diagnostics;

namespace SystemReadinessCore.Libraries.ProcessManager
{
    public class NewProcess
    {
        private protected static int ExitCode { get; private set; }

        public static int Run(string FileName, string? Args, bool RunAsAdministrator = false)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = FileName;
                if (Args != null) { process.StartInfo.Arguments = Args; }
                process.StartInfo.ErrorDialog = true;

                switch (RunAsAdministrator)
                {
                    case true:
                        process.StartInfo.Verb = "runas";
                        process.StartInfo.UseShellExecute = true;
                        break;
                    default:
                        process.StartInfo.UseShellExecute = false;
                        break;
                }

                process.Start();
                process.WaitForExit();
                ExitCode = process.ExitCode;
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
            catch (SystemException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return ExitCode;
        }
    }
}
