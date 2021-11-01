using System.Net;

using Iswenzz.GitTools.Utils;

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
    }
}
