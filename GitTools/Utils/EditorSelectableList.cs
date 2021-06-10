using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Iswenzz.GitTools.Utils
{
    /// <summary>
    /// Open a temporary file in the default editor to pick all lines to select.
    /// </summary>
    public class EditorSelectableList : IDisposable
    {
        public string FilePath { get; set; }
        public Process Process { get; set; }

        /// <summary>
        /// Initialize a new EditorSlectableList instance.
        /// </summary>
        public EditorSelectableList()
        {
            FilePath = $"{Path.GetTempFileName()}.txt";
        }

        /// <summary>
        /// Get the selected lines from the editor.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetFileContent()
        {
            Process.WaitForExit();
            return File.ReadAllLines(FilePath);
        }

        /// <summary>
        /// Open the editor with the specified content.
        /// </summary>
        /// <param name="lines">The editor content.</param>
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

        /// <summary>
        /// Release resources.
        /// </summary>
        public void Dispose()
        {
            Process?.Dispose();
        }
    }
}
