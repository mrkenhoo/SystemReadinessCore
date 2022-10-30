using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class TaskManager
    {
        public static int ExitCode { get; set; }

        public static void CreateTask(string TaskName,
                                      string ExecutableFile,
                                      string Arguments,
                                      string? WorkingDirectory,
                                      string Description,
                                      short NumberOfDays)
        {
            if (PrivilegesManager.IsUserAdmin())
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
                    if (ExitCode == 1)
                    {
                        MessagesManager.ShowMessage(messageBoxText: $"Could not create the task {TaskName}.",
                                                    caption: "Error",
                                                    button: MessageBoxButton.OK,
                                                    icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        MessagesManager.ShowMessage(messageBoxText: $"The task {TaskName} has been created or updated.",
                                                    caption: "Task created",
                                                    button: MessageBoxButton.OK,
                                                    icon: MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessagesManager.ShowMessage(messageBoxText: $"Cannot create the task {TaskName}. {ex.Message}",
                                                caption: ex.Source,
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);
                }
            }
            else
            {
                MessagesManager.ShowMessage(messageBoxText: $"You need administrator privileges to create the task {TaskName}.",
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }

        public static void RemoveTask(string TaskName)
        {
            if (PrivilegesManager.IsUserAdmin())
            {
                TaskService ts = new();

                try
                {
                    ts.RootFolder.DeleteTask(TaskName, exceptionOnNotExists: true);
                    if (ExitCode == 1)
                    {
                        MessagesManager.ShowMessage(messageBoxText: $"Could not remove the task {TaskName}.",
                                                    caption: "Error",
                                                    button: MessageBoxButton.OK,
                                                    icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        MessagesManager.ShowMessage(messageBoxText: $"The task {TaskName} has been deleted.",
                                                    caption: "Task deleted",
                                                    button: MessageBoxButton.OK,
                                                    icon: MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessagesManager.ShowMessage(messageBoxText: $"Could not remove the task {TaskName}. {ex.Message}",
                                                caption: ex.Source,
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);
                }
            }
            else
            {
                MessagesManager.ShowMessage(messageBoxText: $"You need administrator privileges to delete the task {TaskName}.",
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
    }
}
