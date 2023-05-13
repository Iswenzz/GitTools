using Iswenzz.GitTools.CLI;

namespace Iswenzz.GitTools
{
    /// <summary>
    /// The program main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The program entry point.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        public static void Main(string[] args)
        {
            CLIParser.Parse(args);
        }
    }
}
