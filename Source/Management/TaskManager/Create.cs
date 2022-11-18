using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows;
using SystemReadinessCore.Libraries.MessagesManager;
using SystemReadinessCore.Management.PrivilegesManager;

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
            if (GetPrivileges.IsUserAdmin())
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
                        });

                    td.Actions.Add(
                        new ExecAction(
                                ExecutableFile,
                                Arguments,
                                WorkingDirectory)
                        );

                    ts.RootFolder.RegisterTaskDefinition(TaskName, td);
                    if (Environment.ExitCode == 1)
                    {
                        NewMessage.Show(messageBoxText: $"Could not create the task {TaskName}.",
                                        caption: "Error",
                                        button: MessageBoxButton.OK,
                                        icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        NewMessage.Show(messageBoxText: $"The task {TaskName} has been created or updated.",
                                        caption: "Task created",
                                        button: MessageBoxButton.OK,
                                        icon: MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    NewMessage.Show(messageBoxText: $"Cannot create the task {TaskName}. {ex.Message}",
                                    caption: ex.Source,
                                    button: MessageBoxButton.OK,
                                    icon: MessageBoxImage.Error);
                }
            }
            else
            {
                NewMessage.Show(messageBoxText: $"You need administrator privileges to create the task {TaskName}.",
                                caption: "Error",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
        }
    }
}
