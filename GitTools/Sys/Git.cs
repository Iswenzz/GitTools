using Iswenzz.GitTools.Data;
using LibGit2Sharp;
using System;

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
            Repository = new Repository(repoPath);
        }

        /// <summary>
        /// Commit to the git repository with the specified GitCommit object.
        /// </summary>
        /// <param name="commit">The commit informations.</param>
        public void Commit(GitCommit commit)
        {
            CommitOptions options = new CommitOptions
            {
                AllowEmptyCommit = true
            };
            Repository.Commit(commit.Message, commit.Author, commit.Committer, options);
        }

        /// <summary>
        /// Commit to the git repository with the specified GitCommit object.
        /// </summary>
        /// <param name="message">The commit message.</param>
        /// <param name="date">The commit date.</param>
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
