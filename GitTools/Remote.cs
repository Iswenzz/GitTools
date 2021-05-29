using System;
using System.Net;
using System.Collections.Generic;
using LibGit2Sharp;

namespace Iswenzz.GitTools
{
    public abstract class Remote : IDisposable
    {
        public virtual WebClient WebClient { get; set; } = new WebClient();

        public abstract List<Commit> GetUserCommits();

        public virtual void Dispose()
        {
            WebClient.Dispose();
        }
    }
}
