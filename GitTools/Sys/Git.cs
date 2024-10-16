using System;
using System.Collections.Generic;
using LibGit2Sharp;

namespace GitTools.Sys
{
    /// <summary>
    /// Class to manipulate git.
    /// </summary>
    public class Git
    {
        public Repository Repository { get; set; }
        public string User { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Initialize a new Git instance with the specified git repository.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="email">The email.</param>
        /// <param name="repository">Git repository path.</param>
        public Git(string user, string email, string repository)
        {
            Repository = new(repository);
            User = user;
            Email = email;
        }

        /// <summary>
        /// Add a remote with a specific name and url, then update the repository remotes.
        /// </summary>
        /// <param name="name">The remote name.</param>
        /// <param name="url">The remote URL.</param>
        public void AddRemote(string name, string url)
        {
            if (Repository.Network.Remotes[name] == null)
            {
                Repository.Network.Remotes.Add(name, url);
                FetchRemote(name);
            }
        }

        /// <summary>
        /// Fetch a remote.
        /// </summary>
        /// <param name="name">The remote name.</param>
        public void FetchRemote(string name) =>
            Repository.Network.Fetch(name, Array.Empty<string>());

        /// <summary>
        /// Remove a specific remote.
        /// </summary>
        /// <param name="name">The remote name.</param>
        public void RemoveRemote(string name) =>
            Repository.Network.Remotes.Remove(name);

        /// <summary>
        /// Predicate to check if an object exists in this repository.
        /// </summary>
        /// <typeparam name="T">Class extending <see cref="GitObject"/>.</typeparam>
        /// <param name="obj">The <see cref="GitObject"/>.</param>
        /// <returns></returns>
        public bool ObjectExists<T>(T obj) where T : GitObject =>
            Repository.Lookup(obj.Id) != null;

        /// <summary>
        /// Get the commits in a specific time range and can be filtered by email. 
        /// Working with squashed commits as well.
        /// </summary>
        public List<Commit> GetCommits(DateTime sinceDate = default, DateTime untilDate = default,
            string email = null)
        {
            DateTime since = sinceDate;
            DateTime until = untilDate != DateTime.UnixEpoch ? untilDate : DateTime.Now;

            List<Commit> commits = [];
            foreach (GitObject o in Repository.ObjectDatabase)
            {
                try
                {
                    Commit c = o.Peel<Commit>();
                    if (!string.IsNullOrEmpty(email) && c.Committer.Email != email)
                        continue;
                    if (c.Committer.When < since || c.Committer.When > until)
                        continue;
                    commits.Add(c);
                }
                catch (InvalidSpecificationException) { }
            }
            return commits;
        }

        /// <summary>
        /// Cherry-pick a specific commit.
        /// </summary>
        /// <param name="commit">The commit to cherry-pick.</param>
        /// <param name="empty">Commit as empty.</param>
        public void CherryPick(Commit commit, bool empty = false)
        {
            try
            {
                if (empty) throw new EmptyCommitException();

                Repository.Reset(ResetMode.Hard);
                CherryPickOptions options = new() { FileConflictStrategy = CheckoutFileConflictStrategy.Theirs };
                CherryPickResult result = Repository.CherryPick(commit, commit.Committer, options);

                if (result.Status == CherryPickStatus.Conflicts)
                {
                    Commands.Stage(Repository, "*");
                    Repository.Commit(commit.Message, commit.Author, commit.Committer);
                }
            }
            catch (EmptyCommitException)
            {
                MockCommit(commit.Message, commit.Committer.When.DateTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Create an empty commit to the git repository.
        /// </summary>
        /// <param name="user">The user to commit.</param>
        /// <param name="message">The commit message.</param>
        /// <param name="date">The commit date.</param>
        public void MockCommit(string message, DateTime date = default)
        {
            if (date == DateTime.MinValue)
                date = DateTime.Now;

            Signature author = new(User, Email, date);
            Signature committer = author;

            CommitOptions options = new()
            {
                AllowEmptyCommit = true
            };
            Repository.Commit(message, author, committer, options);
        }
    }
}
