using System;
using System.Diagnostics;
using SystemReadinessCore.Management.PrivilegesManager;

namespace SystemReadinessCore.Libraries.ProcessManager
{
    public class NewProcess
    {
        public static int Run(string FileName, string? Args, bool RunAsAdministrator = false)
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
                }
                process.Start();
                process.WaitForExit();
                process.Close();
                process.Dispose();

                return process.ExitCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
