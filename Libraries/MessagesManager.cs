using System;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class MessagesManager
    {
        public static void ShowMessage(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            try
            {
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            catch (Exception ex)
            {
                ShowMessage(messageBoxText: ex.Message,
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
    }
}
