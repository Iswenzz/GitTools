using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CommandLine;
using CommandLine.Text;

using Iswenzz.GitTools.Data;
using Iswenzz.GitTools.Remotes;
using Iswenzz.GitTools.Sys;
using Iswenzz.GitTools.Utils;

namespace Iswenzz.GitTools.CLI
{
    /// <summary>
    /// Command to copy commits from one repository to another.
    /// </summary>
    [Verb("copycommits", HelpText = "Copy commits from one repository to another.")]
    public class CopyCommits : ICMD
    {
        [Option('i', "input-repository", Required = true, HelpText = "The input repository path.")]
        public string InputRepository { get; set; }

        [Option('o', "output-repository", Required = true, HelpText = "The output repository path.")]
        public string OutputRepository { get; set; }

        [Option('s', "since-date", HelpText = "Get commits since a specific date.")]
        public string SinceDate { get; set; } = DateTime.MinValue.ToString("O");

        [Option('u', "until-date", HelpText = "Get commits until a specific date.")]
        public string UntilDate { get; set; } = DateTime.Now.ToString("O");

        [Usage(ApplicationAlias = "gittools")]
        public static IEnumerable<Example> Examples
        {
            get => new List<Example> {
                new Example("Copy the commits on a specific date",
                    new CopyCommits {
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
            // Get the user commits
            CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");
            Git inputGit = new(InputRepository);
            List<GitCommit> commits = inputGit.GetUserCommits(
                Program.Settings.User,
                DateTime.Parse(SinceDate, culture),
                DateTime.Parse(UntilDate, culture));

            // Opens a temporary file in the default editor to pick all commits to copy
            IEnumerable<string> commitLines = commits
                .Select(c => $"{c.ID} {c.MessageShort}");
            using EditorSelectableList editor = new();
            editor.OpenWithContent(commitLines);

            IEnumerable<string> selectedCommitLines = editor.GetFileContent();
            IEnumerable<GitCommit> selectedCommits = commits
                .Where(c => selectedCommitLines.Any(l => l.Contains(c.ID)));

            // Commits to output repository
            Git outputGit = new(OutputRepository);
            foreach (GitCommit commit in commits)
                outputGit.Commit(commit);
        }
    }
}
