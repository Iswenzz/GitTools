using CommandLine;

namespace Iswenzz.GitTools.CLI
{
    public static class CLIParser
    {
        public static Options Options { get; set; }
        public static CopyCommitsOptions CopyCommitsOptions { get; set; }

        public static void Parse(string[] args)
        {
            Parser.Default.ParseArguments<CopyCommitsOptions, Options>(args)
                .WithParsed<Options>(options => Options = options)
                .WithParsed<CopyCommitsOptions>(options => CopyCommitsOptions = options);
        }
    }
}
