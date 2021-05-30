using System;
using System.Collections.Generic;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using Iswenzz.GitTools.Remotes;
using Iswenzz.GitTools.Utils;
using LibGit2Sharp;

namespace Iswenzz.GitTools.CLI
{
    [Verb("copycommits", HelpText = "Copy commits from one repository to another")]
    public class CopyCommits : ICommand
    {
        [Option('r', "remote", Required = true, HelpText = "The remote software")]
        public string Remote { get; set; }

        [Option('i', "input-url", Required = true, HelpText = "The input repository URL")]
        public string InputURL { get; set; }

        [Option('o', "output-path", Required = true, HelpText = "The output repository path")]
        public string OutputPath { get; set; }

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

        public void Execute()
        {
            // Get the remote class to call the GetUserCommits method
            Type remoteClassType = Reflect.RetrieveTypeFromCustomAttributeMetadata(
                $"{nameof(GitRemoteAttribute)}-{Remote}");
            AbstractRemote remoteInstance = (AbstractRemote)Activator.CreateInstance(remoteClassType);
            MethodInfo getUserCommits = remoteClassType.GetMethod("GetUserCommits");

            // Get the commits
            List<Commit> commits = (List<Commit>)getUserCommits.Invoke(remoteInstance, null);
        }
    }
}
