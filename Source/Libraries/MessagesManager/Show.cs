using System;
using System.Windows;

namespace SystemReadinessCore.Libraries.MessagesManager
{
    public static class NewMessage
    {
        public static MessageBoxResult Show(string messageBoxText,
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
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
    }
}
