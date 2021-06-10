using Iswenzz.GitTools.CLI;
using Iswenzz.GitTools.Sys;

namespace Iswenzz.GitTools
{
    public static class Program
    {
        public static Settings Settings { get; set; }

        public static void Main(string[] args)
        {
            CLIParser.Parse(args);
            Settings = new Settings();
        }
    }
}
