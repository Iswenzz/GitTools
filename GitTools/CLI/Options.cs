using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace Iswenzz.GitTools.CLI
{
    [Verb("copycommits", HelpText = "Copy commits from one repository to another")]
    public class CopyCommitsOptions
    {
        [Option('m', "method", Required = true, HelpText = "The repository methods i.e: \"gitlab:git\"")]
        public string Method { get; set; }

        [Option('i', "input-url", Required = true, HelpText = "The input repository URL")]
        public string InputURL { get; set; }

        [Option('o', "output-url", Required = true, HelpText = "The output repository URL")]
        public string OutputURL { get; set; }

        [Usage(ApplicationAlias = "gittools")]
        public static IEnumerable<Example> Examples
        {
            get => new List<Example> {
                new Example("Copy the commits on a specific date, from a gitlab repository to a git repository", 
                    new CopyCommitsOptions { 
                        Method = "gitlab:git", 
                        InputURL = "https://gitlab.com/gitlab-org/gitlab", 
                        OutputURL = @"C:\Repository" 
                    })
            };
        }
    }

    public class Options { }
}
