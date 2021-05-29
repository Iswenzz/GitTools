using System;
using System.Collections.Generic;
using System.Reflection;
using Iswenzz.GitTools.CLI;
using Iswenzz.GitTools.Remotes;
using Iswenzz.GitTools.Sys;

namespace Iswenzz.GitTools
{
    public static class Program
    {
        public static Settings Settings { get; set; }

        public static Git Git { get; set; }
        public static GitLab GitLab { get; set; }

        public static void Main(string[] args)
        {
            CLIParser.Parse(args);

            Settings = new Settings();
            GitLab = new GitLab();

            //var a = GitLab.GetUserCommits();
            //a.ForEach((c) => Console.WriteLine(c.Message));

            //Console.WriteLine(CLIParser.Options.Methods);
            //Console.WriteLine(CLIParser.Options.Verbose);
        }
    }
}
