using BrokeProtocol.API;
using Newtonsoft.Json;
using System.IO;

namespace BrokeProtocol.GameSource
{
    public class Core : Plugin
    {
        public static Core Instance { get; internal set; }
        public Settings Settings { get; set; }

        public Core()
        {
            var path = Path.Combine("DMBP", "settings.json");
            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));

            Instance = this;
            Info = new PluginInfo("Deathmatch v0.1", "gamesource")
            {
                Description = "Credits by Logic#8273 ",
                Website = "www.brokeprotocol.com"
            };
        }
    }
}
