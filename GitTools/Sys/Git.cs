using LibGit2Sharp;
using System;

namespace Iswenzz.GitTools.Sys
{
    public class Git
    {
        public Repository Repository { get; set; }

        public Git(string repoPath)
        {
            Repository = new Repository(repoPath);
        }

        public void Commit(string message, DateTime date = default)
        {
            if (date == DateTime.MinValue)
                date = DateTime.Now;

            Signature author = new Signature(
                Program.Settings.User.Username, 
                Program.Settings.User.EMail,
                date);
            Signature committer = author;

            CommitOptions options = new CommitOptions
            {
                AllowEmptyCommit = true
            };
            Repository.Commit(message, author, committer, options);
        }
    }
}
