using CommandLine;

namespace Iswenzz.GitTools.CLI
{
    /// <summary>
    /// Class for parsing the program arguments.
    /// </summary>
    public static class CLIParser
    {
        public static Options Options { get; set; }
        public static CopyCommits CopyCommits { get; set; }
        public static MockCommit Commit { get; set; }

        /// <summary>
        /// Parse the program arguments.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        public static void Parse(string[] args)
        {
            Parser.Default.ParseArguments<Options, CopyCommits, MockCommit>(args)
                .WithParsed<Options>(options => ParseAndExecute(Options = options))
                .WithParsed<CopyCommits>(options => ParseAndExecute(CopyCommits = options))
                .WithParsed<MockCommit>(options => ParseAndExecute(Commit = options));
        }

        /// <summary>
        /// Parse the arguments using the CommandLineParser library.
        /// </summary>
        /// <typeparam name="T">Class that implements ICommand interface.</typeparam>
        /// <param name="options">The parsed options.</param>
        private static void ParseAndExecute<T>(T options) where T : ICMD =>
            options.Execute();
    }
}
