using System.IO;
using Newtonsoft.Json;

namespace Iswenzz.GitTools
{
    public class Settings
    {
        public User User { get; set; }

        public Settings()
        {
            // Load settings.json
            if (!File.Exists("settings.json"))
                File.WriteAllText("settings.json", "");
            string json = File.ReadAllText("settings.json");

            // Deserialize settings.json
            User = JsonConvert.DeserializeObject<User>(json);
            if (User == null)
            {
                json = JsonConvert.SerializeObject(new User());
                File.WriteAllText("settings.json", json);
                User = JsonConvert.DeserializeObject<User>(json);
            }
        }
    }
}
