using System;

namespace Iswenzz.GitTools
{
    public static class Program
    {
        public static Git Git { get; set; }
        public static Settings Settings { get; set; }

        public static void Main(string[] args)
        {
            Settings = new Settings();
            
        }
    }
}
