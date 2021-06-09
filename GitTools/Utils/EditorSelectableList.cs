using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Iswenzz.GitTools.Utils
{
    public class EditorSelectableList : IDisposable
    {
        public string FilePath { get; set; }
        public Process Process { get; set; }

        public EditorSelectableList()
        {
            FilePath = $"{Path.GetTempFileName()}.txt";
        }

        public IEnumerable<string> GetFileContent()
        {
            Process.WaitForExit();
            return File.ReadAllLines(FilePath);
        }

        public void OpenWithContent(IEnumerable<string> lines)
        {
            File.WriteAllLines(FilePath, lines);
            Process = new Process
            {
                StartInfo = new ProcessStartInfo(FilePath)
                {
                    UseShellExecute = true
                }
            };
            Process.Start();
        }

        public void Dispose()
        {
            Process?.Dispose();
        }
    }
}
