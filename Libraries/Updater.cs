using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Windows;

namespace SystemReadinessCore.Libraries
{
    public class Updater
    {
        public static async void GetLatestRelease(string username, string repoName, string assemblyVersion)
        {
            string DownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp";

            try
            {
                GitHubClient ghclient = new(new ProductHeaderValue(username));
                IReadOnlyList<Release> releases = (IReadOnlyList<Release>) await ghclient.Repository.Release.GetAll(username, repoName);

                Version latestVersion = new(releases[0].TagName);
                Version currentVersion = new(assemblyVersion);

                HttpClient httpClient = new();
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla");

                int UpdateAvailable = currentVersion.CompareTo(latestVersion);

                if (UpdateAvailable < 0)
                {
                    MessageBoxResult messageBoxResult = MessagesManager.ShowMessage(messageBoxText: $"There is a new version available: {latestVersion}." +
                                                                                                     "Do you want to download it and install it now?",
                                                                                    caption: "Upgrade available",
                                                                                    button: MessageBoxButton.YesNo,
                                                                                    icon: MessageBoxImage.Information);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        try
                        {
                            byte[] file = await ghclient.Repository.Content.GetArchive(owner: username, name: repoName, archiveFormat: ArchiveFormat.Zipball);

                            await File.WriteAllBytesAsync(DownloadPath, file);

                            ZipFile.ExtractToDirectory(file.ToString(), DownloadPath);

                            ProcessManager.NewProcess(fileName: releases[0].Name, args: null);
                        }
                        catch (Exception ex)
                        {

                            MessagesManager.ShowMessage(messageBoxText: ex.Message,
                                    caption: ex.Source,
                                    button: MessageBoxButton.OK,
                                    icon: MessageBoxImage.Error);
                        }
                    }
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
