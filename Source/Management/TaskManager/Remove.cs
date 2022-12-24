using Microsoft.Win32.TaskScheduler;
using System;
using SystemReadinessCore.Libraries.PrivilegesManager;

namespace SystemReadinessCore.Management.TaskManager
{
    public partial class NewTask
    {
        public static void Remove(string TaskName)
        {
            if (GetPrivileges.IsUserAdmin())
            {
                try
                {
                    TaskService ts = new();
                    ts.RootFolder.DeleteTask(TaskName, exceptionOnNotExists: true);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
