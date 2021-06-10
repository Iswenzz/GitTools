namespace Iswenzz.GitTools.Sys
{
    /// <summary>
    /// User structure for the settings JSON.
    /// </summary>
    public class User
    {
        public string Username { get; set; } = "";
        public string EMail { get; set; } = "";

        public User() { }
        public User(string username, string email)
        {
            Username = username;
            EMail = email;
        }
    }
}
