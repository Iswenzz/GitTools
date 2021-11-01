using System;
using System.Net;

namespace Iswenzz.GitTools.Remotes
{
    /// <summary>
    /// Represent a git remote with common operations.
    /// </summary>
    public abstract class AbstractRemote : IDisposable
    {
        public virtual WebClient WebClient { get; set; } = new WebClient();

        /// <summary>
        /// Dispose resources.
        /// </summary>
        public virtual void Dispose()
        {
            WebClient.Dispose();
        }
    }
}
