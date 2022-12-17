using Octokit;
using System;

namespace SystemReadinessCore.Utilities.UpdatesManager
{
    public partial class Updater
    {
        private static int NewUpdateAvailable { get; set; }

        public static async void GetLatestRelease(string username, string repoName, string assemblyVersion)
        {
            try
            {
                GitHubClient ghclient = new(new ProductHeaderValue(username));
                Release latestRelease = await ghclient.Repository.Release.GetLatest(username, repoName);

                Version latestVersion = new(latestRelease.TagName);
                Version currentVersion = new(assemblyVersion);
                Uri latestVersionUrl = new(latestRelease.AssetsUrl);

                NewUpdateAvailable = currentVersion.CompareTo(latestVersion);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
