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
    [GitRemote("gitlab")]
    public class GitLab : AbstractRemote
    {
        public string API_V4 { get; set; } = "https://gitlab.com/api/v4";

        public override IEnumerable<GitCommit> GetUserCommits()
        {
            string since = DateTime.Parse(CLIParser.CopyCommitsOptions.SinceDate).ToString("O");
            string id = HttpUtility.UrlEncode(CLIParser.CopyCommitsOptions.InputURL);
            string url = $"{API_V4}/projects/{id}/repository/commits?since={since}";

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
                        DateTime.Parse((string)commit.committed_date)),
                    Author = new Signature(
                        (string)commit.author_name,
                        (string)commit.author_email,
                        DateTime.Parse((string)commit.authored_date))
                };
            }
        }
    }
}
