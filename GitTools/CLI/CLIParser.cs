using CommandLine;

namespace Iswenzz.GitTools.CLI
{
    public static class CLIParser
    {
        public static Options Options { get; set; }
        public static CopyCommits CopyCommitsOptions { get; set; }

        public static void Parse(string[] args)
        {
            Parser.Default.ParseArguments<CopyCommits, Options>(args)
                .WithParsed<Options>(options => ParseAndExecute(Options = options))
                .WithParsed<CopyCommits>(options => ParseAndExecute(CopyCommitsOptions = options));
        }

        private static void ParseAndExecute<T>(T options) where T : ICommand => 
            options.Execute();
    }
}
