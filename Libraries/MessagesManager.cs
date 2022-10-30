using System;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class MessagesManager
    {
        public static void ShowMessage(string messageBoxText,
                                       string? caption,
                                       MessageBoxButton button,
                                       MessageBoxImage icon,
                                       bool validateUserInput = false)
        {
            try
            {
                if (!validateUserInput)
                {
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(messageBoxText: ex.Message,
                                caption: ex.Source,
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
        }
    }
}
