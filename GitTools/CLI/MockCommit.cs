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
    [Verb("commit", HelpText = "Create a commit at a specific date.")]
    public class MockCommit : ICMD
    {
        [Option('u', "user", Required = true, HelpText = "The user.")]
        public string User { get; set; }

        [Option('e', "email", Required = true, HelpText = "The email.")]
        public string Email { get; set; }

        [Option('m', "message", Required = true, HelpText = "The commit message.")]
        public string Message { get; set; }

        [Option('o', "output-repository", Required = true, HelpText = "The output repository path.")]
        public string OutputRepository { get; set; }

        [Option('d', "date", HelpText = "Commit at a specific date.")]
        public string Date { get; set; } = DateTime.Now.ToString("O");

        [Usage(ApplicationAlias = "gittools")]
        public static IEnumerable<Example> Examples
        {
            get => [
                new("Create a commit at a specific date",
                    new MockCommit
                    {
                        User = "Iswenzz",
                        Email = "alexisnardiello@gmail.com",
                        OutputRepository = "C:\\Repository",
                        Message = "Happy Birthday !",
                        Date = "25/06/2021"
                    })
            ];
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        public void Execute()
        {
            // Commits to output repository
            Git git = new(User, Email, OutputRepository);
            git.MockCommit(Message, DateTime.Parse(Date));
        }
    }
}
