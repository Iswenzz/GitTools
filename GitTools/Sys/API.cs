namespace Iswenzz.GitTools.Sys
{
    public class API
    {
        public string GITLAB_API_KEY { get; set; } = "";

        public API() { }
        public API(string GITLAB_API_KEY)
        {
            this.GITLAB_API_KEY = GITLAB_API_KEY;
        }
    }
}
