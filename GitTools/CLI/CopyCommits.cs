using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CommandLine;
using CommandLine.Text;
using LibGit2Sharp;

using Iswenzz.GitTools.Sys;
using Iswenzz.GitTools.Utils;
using System.IO;

namespace Iswenzz.GitTools.CLI
{
    /// <summary>
    /// Command to copy commits from one repository to another.
    /// </summary>
    [Verb("copycommits", HelpText = "Copy commits from one repository to another.")]
    public class CopyCommits : ICMD
    {
        [Option('u', "user", Required = true, HelpText = "The user.")]
        public string User { get; set; }

        [Option('e', "email", Required = true, HelpText = "The email.")]
        public string Email { get; set; }

        [Option('f', "filter", HelpText = "Filter the commits by email.")]
        public string Filter { get; set; }

        [Option('i', "input-repository", Required = true, HelpText = "The input repository path.")]
        public string InputRepository { get; set; }

        [Option('o', "output-repository", Required = true, HelpText = "The output repository path.")]
        public string OutputRepository { get; set; }

        [Option("since-date", HelpText = "Get commits since a specific date.")]
        public string SinceDate { get; set; } = DateTime.UnixEpoch.ToString("O");

        [Option("until-date", HelpText = "Get commits until a specific date.")]
        public string UntilDate { get; set; } = DateTime.Now.ToString("O");

        [Option("empty", HelpText = "Copy as empty commit.")]
        public bool Empty { get; set; }

        [Usage(ApplicationAlias = "gittools")]
        public static IEnumerable<Example> Examples
        {
            get => new List<Example> {
                new Example("Copy the commits on a specific date",
                    new CopyCommits {
                        User = "Iswenzz",
                        Email = "alexisnardiello@gmail.com",
                        InputRepository = "C:\\Repository\\A",
                        OutputRepository = "C:\\Repository\\B",
                        SinceDate = "25/06/1999",
                        UntilDate = "04/07/2021"
                    })
            };
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        public void Execute()
        {
            Git input = new(User, Email, InputRepository);
            Git output = new(User, Email, OutputRepository);

            string inputRepositoryName = Path.GetFileName(InputRepository).Replace(" ", "_");
            CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");

            // Get the user commits
            List<Commit> commits = input.GetCommits(
                DateTime.Parse(SinceDate, culture),
                DateTime.Parse(UntilDate, culture),
                Filter);

            // Add the input repository as a remote in the output repository to cherry pick commits
            output.AddRemote(inputRepositoryName, InputRepository);
            output.FetchRemote(inputRepositoryName);

            // Opens a temporary file in the default editor to pick all commits to copy
            using Editor editor = new();
            List<Commit> selectedCommits = editor.SelectCommits(commits, output);

            // Cherry-pick selected commits to the output repository
            foreach (Commit commit in selectedCommits)
                output.CherryPick(commit, Empty);
        }
    }
}
