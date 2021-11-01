using LibGit2Sharp;
using System;
using System.Linq;
using System.Collections.Generic;

using Iswenzz.GitTools.Data;

namespace Iswenzz.GitTools.Sys
{
    /// <summary>
    /// Class to manipulate git.
    /// </summary>
    public class Git
    {
        public Repository Repository { get; set; }

        /// <summary>
        /// Initialize a new Git instance with the specified git repository.
        /// </summary>
        /// <param name="repoPath">Git repository path.</param>
        public Git(string repoPath)
        {
            Repository = new(repoPath);
        }

        /// <summary>
        /// Get the user's commits (working with squashed commits).
        /// </summary>
        public List<GitCommit> GetUserCommits(User user,
            DateTime sinceDate = default, DateTime untilDate = default)
        {
            DateTime since = sinceDate;
            DateTime until = untilDate != DateTime.MinValue ? untilDate : DateTime.Now;

            List<GitCommit> userCommits = new();
            foreach (GitObject o in Repository.ObjectDatabase)
            {
                try
                {
                    Commit c = o.Peel<Commit>();
                    if (c.Committer.Email == user.EMail &&
                        c.Committer.When >= since && c.Committer.When <= until)
                        userCommits.Add(new GitCommit(c));
                }
                catch { }
            }
            return userCommits;
        }

        /// <summary>
        /// Commit to the git repository with the specified GitCommit object.
        /// </summary>
        /// <param name="commit">The commit informations.</param>
        public void Commit(GitCommit commit)
        {
            CommitOptions options = new()
            {
                AllowEmptyCommit = true
            };
            Repository.Commit(commit.Message, commit.Author, commit.Committer, options);
        }

        /// <summary>
        /// Commit to the git repository with the specified GitCommit object.
        /// </summary>
        /// <param name="user">The user to commit.</param>
        /// <param name="message">The commit message.</param>
        /// <param name="date">The commit date.</param>
        public void Commit(User user, string message, DateTime date = default)
        {
            if (date == DateTime.MinValue)
                date = DateTime.Now;

            Signature author = new(user.Username, user.EMail, date);
            Signature committer = author;

            CommitOptions options = new()
            {
                AllowEmptyCommit = true
            };
            Repository.Commit(message, author, committer, options);
        }
    }
}
