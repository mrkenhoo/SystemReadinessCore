using System;
using System.Diagnostics;
using SystemReadinessCore.Source.Management.PrivilegesManager;

namespace SystemReadinessCore.Source.Libraries.ProcessManager
{
    public class NewProcess
    {
        public static int Run(string FileName, string? Args, bool RunAsAdministrator = false)
        {
            try
            {
                if (!GetPrivileges.IsUserAdmin()) { throw new AccessViolationException(); }

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
