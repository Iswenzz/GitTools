using System;
using System.Net;
using System.Collections.Generic;
using Iswenzz.GitTools.Data;

namespace Iswenzz.GitTools.Remotes
{
    public abstract class AbstractRemote : IDisposable
    {
        public virtual WebClient WebClient { get; set; } = new WebClient();

        public abstract IEnumerable<GitCommit> GetUserCommits();

        public virtual void Dispose()
        {
            WebClient.Dispose();
        }
    }
}
