using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Iswenzz.GitTools.CLI;
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
        /// Get the user's commits.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<GitCommit> GetUserCommits()
        {
            string since = DateTime.Parse(CLIParser.CopyCommitsOptions.SinceDate).ToString("O");
            string until = DateTime.Parse(CLIParser.CopyCommitsOptions.UntilDate).ToString("O");
            string id = HttpUtility.UrlEncode(CLIParser.CopyCommitsOptions.InputURL);
            string url = $"{API_V4}/projects/{id}/repository/commits?since={since}&until={until}";

            string response = WebClient.DownloadString(url);
            dynamic json = JToken.Parse(response);

            foreach (dynamic commit in json)
            {
                yield return new GitCommit
                {
                    Id = (string)commit.id,
                    Message = (string)commit.title,
                    MessageShort = (string)commit.message,
                    Committer = new Signature(
                        (string)commit.committer_name,
                        (string)commit.committer_email,
                        DateTime.ParseExact((string)commit.committed_date, "MM/dd/yyyy HH:mm:ss", null)),
                    Author = new Signature(
                        (string)commit.author_name,
                        (string)commit.author_email,
                        DateTime.ParseExact((string)commit.authored_date, "MM/dd/yyyy HH:mm:ss", null))
                };
            }
        }
    }
}
