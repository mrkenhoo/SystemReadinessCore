using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows;
using SystemReadinessCore.Libraries.MessagesManager;
using SystemReadinessCore.Management.PrivilegesManager;

namespace SystemReadinessCore.Management.TaskManager
{
    public partial class NewTask
    {
        public static void Remove(string TaskName)
        {
            if (GetPrivileges.IsUserAdmin())
            {
                TaskService ts = new();

                try
                {
                    ts.RootFolder.DeleteTask(TaskName, exceptionOnNotExists: true);
                    if (Environment.ExitCode == 1)
                    {
                        NewMessage.Show(messageBoxText: $"Could not remove the task {TaskName}.",
                                        caption: "Error",
                                        button: MessageBoxButton.OK,
                                        icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        NewMessage.Show(messageBoxText: $"The task {TaskName} has been deleted.",
                                        caption: "Task deleted",
                                        button: MessageBoxButton.OK,
                                        icon: MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    NewMessage.Show(messageBoxText: $"Could not remove the task {TaskName}. {ex.Message}",
                                    caption: ex.Source,
                                    button: MessageBoxButton.OK,
                                    icon: MessageBoxImage.Error);
                }
            }
            else
            {
                NewMessage.Show(messageBoxText: $"You need administrator privileges to delete the task {TaskName}.",
                                caption: "Error",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
        }
    }
}
