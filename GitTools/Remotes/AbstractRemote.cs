using System;
using System.Net;
using System.Collections.Generic;
using Iswenzz.GitTools.Data;

namespace Iswenzz.GitTools.Remotes
{
    /// <summary>
    /// Represent a git remote with common operations.
    /// </summary>
    public abstract class AbstractRemote : IDisposable
    {
        public virtual WebClient WebClient { get; set; } = new WebClient();

        /// <summary>
        /// Get the user's commits.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<GitCommit> GetUserCommits();

        /// <summary>
        /// Dispose resources.
        /// </summary>
        public virtual void Dispose()
        {
            WebClient.Dispose();
        }
    }
}
