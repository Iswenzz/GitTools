using System;
using System.Collections.Generic;
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
    [Verb("copycommits", HelpText = "Copy commits from one repository to another")]
    public class CopyCommits : ICommand
    {
        [Option('r', "remote", Required = true, HelpText = "The remote software")]
        public string Remote { get; set; }

        [Option('i', "input-url", Required = true, HelpText = "The input repository URL")]
        public string InputURL { get; set; }

        [Option('o', "output-path", Required = true, HelpText = "The output repository path")]
        public string OutputPath { get; set; }

        [Option('s', "since-date", HelpText = "Get commits since a specific date")]
        public string SinceDate { get; set; } = DateTime.MinValue.ToString("O");

        [Option('u', "until-date", HelpText = "Get commits until a specific date")]
        public string UntilDate { get; set; } = DateTime.Now.ToString("O");

        [Option('e', "filter-email", HelpText = "Filter commits by the email in the settings.json file")]
        public bool FilterEmail { get; set; }

        [Usage(ApplicationAlias = "gittools")]
        public static IEnumerable<Example> Examples
        {
            get => new List<Example> {
                new Example("Copy the commits on a specific date, from a gitlab repository to a git repository",
                    new CopyCommits {
                        Remote = "gitlab",
                        InputURL = "https://gitlab.com/gitlab-org/gitlab",
                        OutputPath = @"C:\Repository"
                    })
            };
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        public void Execute()
        {
            // Get the remote class to call the GetUserCommits method
            Type remoteClassType = Reflect.RetrieveTypeFromCustomAttributeMetadata(
                $"{nameof(GitRemoteAttribute)}-{Remote}");
            AbstractRemote remoteInstance = (AbstractRemote)Activator.CreateInstance(remoteClassType);
            MethodInfo getUserCommits = remoteClassType.GetMethod("GetUserCommits");

            // Get the commits
            List<GitCommit> commits = new List<GitCommit>((IEnumerable<GitCommit>)getUserCommits.Invoke(remoteInstance, null));

            // Opens a temporary file in the default editor to pick all commits to copy
            IEnumerable<string> commitLines = commits.Select(c => $"{c.Id} {c.Author.Email.PadRight(40)} {c.Message}");
            EditorSelectableList editor = new EditorSelectableList();
            editor.OpenWithContent(commitLines);
            IEnumerable<string> selectedCommitLines = editor.GetFileContent();
            IEnumerable<GitCommit> selectedCommits = commits.Where(c => selectedCommitLines.Any(l => l.Contains(c.Id)));

            // Commits to output repository
            Git git = new Git(CLIParser.CopyCommitsOptions.OutputPath);
            foreach (GitCommit commit in commits)
                git.Commit(commit);
        }
    }
}
