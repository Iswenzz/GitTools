using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;
using Iswenzz.GitTools.Sys;

namespace Iswenzz.GitTools.CLI
{
    /// <summary>
    /// Command to create a commit at a specific date.
    /// </summary>
    [Verb("commit", HelpText = "Create a commit at a specific date")]
    public class Commit : ICommand
    {
        [Option('m', "message", Required = true, HelpText = "The commit message")]
        public string Message { get; set; }

        [Option('o', "output-repository", Required = true, HelpText = "The output repository path")]
        public string OutputRepository { get; set; }

        [Option('d', "date", HelpText = "Commit at a specific date")]
        public string Date { get; set; } = DateTime.Now.ToString("O");

        [Usage(ApplicationAlias = "gittools")]
        public static IEnumerable<Example> Examples
        {
            get => new List<Example> {
                new Example("Create a commit at a specific date",
                    new Commit {
                        OutputRepository = "\"C:\\Repository\"",
                        Message = "Happy Birthday !",
                        Date = "25/06/2021"
                    })
            };
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        public void Execute()
        {
            // Commits to output repository
            Git git = new Git(OutputRepository);
            git.Commit(Message, DateTime.Parse(Date));
        }
    }
}
