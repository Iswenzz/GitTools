using LibGit2Sharp;

namespace Iswenzz.GitTools.Data
{
    public struct GitCommit
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string MessageShort { get; set; }
        public string Encoding { get; set; }
        public Signature Author { get; set; }
        public Signature Committer { get; set; }
    }
}
