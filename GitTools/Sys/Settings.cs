using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iswenzz.GitTools.Sys
{
    public class Settings
    {
        public User User { get; set; } = new User();
        public API API { get; set; } = new API();

        public Settings()
        {
            // Load settings.json
            if (!File.Exists("settings.json"))
                File.WriteAllText("settings.json", "");
            string json = File.ReadAllText("settings.json");

            try
            {
                // Deserialize settings.json
                JObject desrializedJSON = JObject.Parse(json);
                User = desrializedJSON["User"].ToObject<User>();
                API = desrializedJSON["API"].ToObject<API>();
            }
            catch { }

            // Save settings.json
            json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText("settings.json", json);
        }
    }
}
