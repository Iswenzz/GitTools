using LibGit2Sharp;

namespace Iswenzz.GitTools.Data
{
    /// <summary>
    /// Commit structure.
    /// </summary>
    public struct GitCommit
    {
        public string ID { get; set; }
        public string Message { get; set; }
        public string MessageShort { get; set; }
        public string Encoding { get; set; }
        public Signature Author { get; set; }
        public Signature Committer { get; set; }

        public GitCommit(Commit commit)
        {
            ID = commit.Id.Sha;
            Message = commit.Message;
            MessageShort = commit.MessageShort;
            Encoding = commit.Encoding;
            Author = commit.Author;
            Committer = commit.Committer;
        }
    }
}
