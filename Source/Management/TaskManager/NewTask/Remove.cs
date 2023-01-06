using Microsoft.Win32.TaskScheduler;
using System;
using System.IO;
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

                    if (ts.FindTask(TaskName) == null)
                    {
                        throw new IOException($"The task {TaskName} does not exist");
                    }

                    ts.RootFolder.DeleteTask(TaskName, exceptionOnNotExists: true);
                    ts.Dispose();
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
