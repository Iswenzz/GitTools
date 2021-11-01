namespace Iswenzz.GitTools.Sys
{
    /// <summary>
    /// User structure for the settings JSON.
    /// </summary>
    public struct User
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string EMail { get; set; }

        public User(string name, string username, string email)
        {
            Name = name;
            Username = username;
            EMail = email;
        }
    }
}
