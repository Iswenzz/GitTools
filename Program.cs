using Iswenzz.GitTools.CLI;
using Iswenzz.GitTools.Sys;

namespace Iswenzz.GitTools
{
    /// <summary>
    /// The program main class.
    /// </summary>
    public static class Program
    {
        public static Settings Settings { get; set; }

        /// <summary>
        /// The program entry point.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        public static void Main(string[] args)
        {
            CLIParser.Parse(args);
            Settings = new Settings();
        }
    }
}
