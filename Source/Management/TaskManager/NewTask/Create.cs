using Microsoft.Win32.TaskScheduler;
using System;
using System.IO;
using SystemReadinessCore.Libraries.PrivilegesManager;

namespace SystemReadinessCore.Management.TaskManager
{
    public partial class NewTask
    {
        public static void Create(string TaskName,
                                 string ExecutableFile,
                                 string Arguments,
                                 string? WorkingDirectory,
                                 string Description,
                                 short NumberOfDays)
        {
            try
            {
                TaskService ts = new();
                TaskDefinition td = ts.NewTask();

                td.RegistrationInfo.Description = Description;

                td.Triggers.Add(
                    new DailyTrigger
                    {
                        DaysInterval = NumberOfDays
                    }
                );

                td.Actions.Add(
                    new ExecAction(
                            ExecutableFile,
                            Arguments,
                            WorkingDirectory)
                );

                if (ts.FindTask(TaskName) != null)
                {
                    throw new IOException($"The task {TaskName} already exists");
                }

                ts.RootFolder.RegisterTaskDefinition(TaskName, td);
                ts.RootFolder.Dispose();
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
        }
    }
}
