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
        /// Get the user's commits from a specific repository.
        /// </summary>
        /// <param name="repositoryId">The repository ID.</param>
        /// <param name="userName">The author username.</param>
        /// <param name="sinceDate">Search commits since a specific date.</param>
        /// <param name="untilDate">Search commits until a specific date.</param>
        /// <returns></returns>
        public abstract IEnumerable<GitCommit> GetRepositoryUserCommits(string repositoryId, string userName,
            DateTime? sinceDate = null, DateTime? untilDate = null);

        /// <summary>
        /// Get a single commit from a specific hash.
        /// </summary>
        /// <param name="repositoryId">The repository ID.</param>
        /// <param name="commitHash">The commits hash.</param>
        /// <returns></returns>
        public abstract GitCommit GetSingleCommit(string repositoryId, string commitHash);

        /// <summary>
        /// Dispose resources.
        /// </summary>
        public virtual void Dispose()
        {
            WebClient.Dispose();
        }
    }
}
