using Octokit;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class Updater
    {
        public static async void GetLatestRelease(string username, string repoName, string assemblyVersion)
        {
            string DownloadPath =
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp";

            try
            {
                GitHubClient ghclient = new(new ProductHeaderValue(username));
                IReadOnlyList<Release> releases =
                    (IReadOnlyList<Release>) await ghclient.Repository.Release.GetAll(username,
                                                                                      repoName);

                Version latestVersion = new(releases[0].TagName);
                Version currentVersion = new(assemblyVersion);

                int UpdateAvailable = currentVersion.CompareTo(latestVersion);

                if (UpdateAvailable < 0)
                {
                    MessagesManager.ShowMessage(messageBoxText: $"There is a new version available: {latestVersion}.",
                                                caption: "Upgrade available",
                                                button: MessageBoxButton.YesNo,
                                                icon: MessageBoxImage.Information);
                }
                else if (UpdateAvailable > 0)
                {
                    MessagesManager.ShowMessage(messageBoxText: "You are using a newer version than the latest release",
                                            caption: "Information",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Information);
                }
                else
                {
                    MessagesManager.ShowMessage(messageBoxText: "There are no updates available",
                                            caption: "No update available",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessagesManager.ShowMessage(messageBoxText: ex.Message,
                                            caption: "Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error);
            }
        }
    }
}
