using Octokit;
using System;
using System.Collections.Generic;
using System.Windows;
using SystemReadinessCore.Libraries.MessagesManager;

namespace SystemReadinessCore.Utilities.UpdatesManager
{
    public partial class Updater
    {
        public static async void GetLatestRelease(string username, string repoName, string assemblyVersion)
        {
            try
            {
                GitHubClient ghclient = new(new ProductHeaderValue(username));
                IReadOnlyList<Release> releases =
                    (IReadOnlyList<Release>) await ghclient.Repository.Release.GetLatest(username,
                                                                                         repoName);

                Version latestVersion = new(releases[0].TagName);
                Version currentVersion = new(assemblyVersion);
                Uri latestVersionUrl = new(releases[0].AssetsUrl);

                int UpdateAvailable = currentVersion.CompareTo(latestVersion);

                if (UpdateAvailable < 0)
                {
                    MessageBoxResult installUpdate =
                        NewMessage.Show(messageBoxText: $"There is a new version available: {latestVersion}. Do you want to install it?",
                                        caption: "Upgrade available",
                                        button: MessageBoxButton.YesNo,
                                        icon: MessageBoxImage.Information,
                                        validateUserInput: true);
                    if (installUpdate == MessageBoxResult.Yes)
                    {
                        try
                        {

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                else if (UpdateAvailable > 0)
                {
                    NewMessage.Show(messageBoxText: "You are using a newer version than the latest release",
                                    caption: "Information",
                                    button: MessageBoxButton.OK,
                                    icon: MessageBoxImage.Information);
                }
                else
                {
                    NewMessage.Show(messageBoxText: "There are no updates available",
                                    caption: "No update available",
                                    button: MessageBoxButton.OK,
                                    icon: MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                NewMessage.Show(messageBoxText: ex.Message,
                                caption: "Error",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            }
        }
    }
}
