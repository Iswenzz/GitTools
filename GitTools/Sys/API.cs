namespace Iswenzz.GitTools.Sys
{
    /// <summary>
    /// API structure used in the settings JSON.
    /// </summary>
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
