using Octokit;
using System;
using System.Threading.Tasks;

namespace SystemReadinessCore.Utilities.UpdatesManager
{
    public partial class Updater
    {
        public static async Task<int> GetLatestRelease(string username, string repoName, string assemblyVersion)
        {
            try
            {
                GitHubClient ghclient = new(new ProductHeaderValue(username));
                Release releases = await ghclient.Repository.Release.GetLatest(username, repoName);

                Version latestVersion = new(releases.TagName);
                Version currentVersion = new(assemblyVersion);
                Uri latestVersionUrl = new(releases.AssetsUrl);

                return currentVersion.CompareTo(latestVersion);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
