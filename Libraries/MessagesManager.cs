using System;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class MessagesManager
    {
        public static MessageBoxResult ShowMessage(string messageBoxText,
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
                    MessageBoxResult messageBoxResult = MessageBox.Show(messageBoxText, caption, button, icon);

                    return messageBoxResult;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(messageBoxText: ex.Message,
                                caption: ex.Source,
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
            return 0;
        }
    }
}
