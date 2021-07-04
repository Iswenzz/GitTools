using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Iswenzz.GitTools.Data;
using Iswenzz.GitTools.Utils;
using LibGit2Sharp;
using Newtonsoft.Json.Linq;

namespace Iswenzz.GitTools.Remotes
{
    /// <summary>
    /// GitLab remote.
    /// </summary>
    [GitRemote("gitlab")]
    public class GitLab : AbstractRemote
    {
        public string API_V4 { get; set; } = "https://gitlab.com/api/v4";

        /// <summary>
        /// Initialize a new GitLab instance.
        /// </summary>
        public GitLab()
        {
            WebClient.Headers.Add("PRIVATE-TOKEN", Program.Settings.API.GITLAB_API_KEY);
        }

        /// <summary>
        /// Get the user's commits from a specific repository.
        /// </summary>
        /// <param name="repositoryId">The repository ID.</param>
        /// <param name="userName">The author username.</param>
        /// <param name="sinceDate">Search commits since a specific date.</param>
        /// <param name="untilDate">Search commits until a specific date.</param>
        /// <returns></returns>
        public override IEnumerable<GitCommit> GetRepositoryUserCommits(string repositoryId, string userName, 
            DateTime? sinceDate = null, DateTime? untilDate = null)
        {
            List<GitCommit> commits = new List<GitCommit>();
            string since = (sinceDate ?? DateTime.MinValue).ToString("O");
            string until = (untilDate ?? DateTime.Today).ToString("O");
            string id = HttpUtility.UrlEncode(repositoryId);
            int pageIndex = 1;

            try
            {
                while (true)
                {
                    string url = $"{API_V4}/projects/{id}/events?action=pushed&after={since}&before={until}&per_page=100&page={pageIndex++}";

                    string response = WebClient.DownloadString(url);
                    dynamic json = JToken.Parse(response);
                    if (json.Count <= 0) break;
                    Console.WriteLine($"GET {url}");

                    foreach (dynamic action in json)
                    {
                        try
                        {
                            if ((string)action.author.username != userName || 
                                string.IsNullOrEmpty((string)action.push_data.commit_to))
                                continue;

                            commits.Add(GetSingleCommit(repositoryId, (string)action.push_data.commit_to));
                        }
                        catch (WebException) { }
                    }
                }
            }
            catch (WebException) { }
            return commits;
        }

        /// <summary>
        /// Get a single commit from a specific hash.
        /// </summary>
        /// <param name="repositoryId">The repository ID.</param>
        /// <param name="commitHash">The commits hash.</param>
        /// <returns></returns>
        public override GitCommit GetSingleCommit(string repositoryId, string commitHash)
        {
            string id = HttpUtility.UrlEncode(repositoryId);
            string url = $"{API_V4}/projects/{id}/repository/commits/{commitHash}";

            string response = WebClient.DownloadString(url);
            dynamic json = JToken.Parse(response);
            Console.WriteLine($"GET {url}");

            return new GitCommit
            {
                Id = (string)json.id,
                Message = (string)json.title,
                Committer = new Signature(
                    (string)json.committer_name,
                    (string)json.committer_email,
                    DateTime.ParseExact((string)json.committed_date, "MM/dd/yyyy HH:mm:ss", null)),
                Author = new Signature(
                    (string)json.author_name,
                    (string)json.author_email,
                    DateTime.ParseExact((string)json.authored_date, "MM/dd/yyyy HH:mm:ss", null))
            };
        }
    }
}
