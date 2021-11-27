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
        [Option('i', "input-repository", Required = true, HelpText = "The input repository path.")]
        public string InputRepository { get; set; }

        [Option('o', "output-repository", Required = true, HelpText = "The output repository path.")]
        public string OutputRepository { get; set; }

        [Option('s', "since-date", HelpText = "Get commits since a specific date.")]
        public string SinceDate { get; set; } = DateTime.MinValue.ToString("O");

        [Option('u', "until-date", HelpText = "Get commits until a specific date.")]
        public string UntilDate { get; set; } = DateTime.Now.ToString("O");

        [Option('e', "email", HelpText = "Filter the commits by email.")]
        public string Email { get; set; }

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
            Git inputGit = new(InputRepository);
            Git outputGit = new(OutputRepository);

            string inputRepositoryName = Path.GetFileName(InputRepository);
            CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");

            // Get the user commits
            List<Commit> commits = inputGit.GetCommits(
                DateTime.Parse(SinceDate, culture),
                DateTime.Parse(UntilDate, culture),
                Email);

            // Add the input repository as a remote in the output repository to cherry pick commits
            outputGit.AddRemote(inputRepositoryName, InputRepository);
            outputGit.FetchRemote(inputRepositoryName);

            // Opens a temporary file in the default editor to pick all commits to copy
            IEnumerable<string> commitLines = commits
                .Where(outputGit.ObjectExists)
                .Select(c => $"{c.Id.Sha} {c.MessageShort}");
            using EditorSelectableList editor = new();
            editor.OpenWithContent(commitLines);

            IEnumerable<string> selectedCommitLines = editor.GetFileContent();
            IEnumerable<Commit> selectedCommits = commits
                .Where(c => selectedCommitLines.Any(l => l.Contains(c.Id.Sha)));

            // Cherry-pick selected commits to the output repository
            foreach (Commit commit in selectedCommits)
                outputGit.CherryPick(commit);
        }
    }
}
