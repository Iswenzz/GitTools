using System;
using System.Collections.Generic;
using System.Net;
using Iswenzz.GitTools.Utils;
using LibGit2Sharp;

namespace Iswenzz.GitTools.Remotes
{
    [GitRemote("gitlab")]
    public class GitLab : AbstractRemote
    {
        public string API_V4 { get; set; } = "https://gitlab.com/api/v4";

        public override List<Commit> GetUserCommits()
        {
            //string response = WebClient.DownloadString($"{API_V4}/projects/:id/repository/commits");
            //Console.WriteLine(response);

            return null;
        }
    }
}
