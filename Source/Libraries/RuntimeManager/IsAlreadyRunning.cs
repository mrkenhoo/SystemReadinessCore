using System;
using System.Diagnostics;
using System.Linq;

namespace SystemReadinessCore.Libraries.RuntimeManager
{
    public partial class Runtime
    {
        public static bool IsAlreadyRunning()
        {
            try
            {
                Process process = Process.GetCurrentProcess();
                Process[] ProcessList = Process.GetProcessesByName(process.ProcessName);

                if (ProcessList != null && ProcessList.Any() && ProcessList.Length > 1)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}
