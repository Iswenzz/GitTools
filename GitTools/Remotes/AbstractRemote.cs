using System;
using System.Net;
using System.Collections.Generic;
using LibGit2Sharp;

namespace Iswenzz.GitTools.Remotes
{
    public abstract class AbstractRemote : IDisposable
    {
        public virtual WebClient WebClient { get; set; } = new WebClient();

        public abstract List<Commit> GetUserCommits();

        public virtual void Dispose()
        {
            WebClient.Dispose();
        }
    }
}
