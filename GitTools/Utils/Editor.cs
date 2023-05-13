using Iswenzz.GitTools.Sys;
using LibGit2Sharp;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Iswenzz.GitTools.Utils
{
    /// <summary>
    /// Open a temporary file in the default editor to pick all lines to select.
    /// </summary>
    public class Editor : IDisposable
    {
        public string FilePath { get; set; }
        public Process Process { get; set; }

        /// <summary>
        /// Initialize a new EditorSlectableList instance.
        /// </summary>
        public Editor()
        {
            FilePath = $"{Path.GetTempFileName()}.txt";
        }

        /// <summary>
        /// Select commits from git repository.
        /// </summary>
        /// <param name="commits">The commits.</param>
        /// <param name="git">The git repository.</param>
        /// <returns></returns>
        public List<Commit> SelectCommits(List<Commit> commits, Git git)
        {
            IEnumerable<string> commitLines = commits
                .Where(git.ObjectExists)
                .Select(c => $"{c.Id.Sha} {c.MessageShort}");

            OpenWithContent(commitLines);
            IEnumerable<string> selectedCommitLines = GetFileContent();
            return commits.Where(c => selectedCommitLines.Any(l => l.Contains(c.Id.Sha))).ToList();
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
                    UseShellExecute = true,
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
