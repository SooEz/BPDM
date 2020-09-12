using BrokeProtocol.API;
using Newtonsoft.Json;
using System.IO;

namespace BrokeProtocol.GameSource
{
    public class Core : Plugin
    {
        public static Core Instance { get; internal set; }
        public Settings Settings { get; set; }
        public string TeamKey { get; set; } = "BPDM:Team";
        
        public Core()
        {
            var path = Path.Combine("BPDM", "settings.json");
            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));

            Instance = this;
            Info = new PluginInfo("Deathmatch v0.1", "BPDM")
            {
                Description = "Credits by Logic#8273 ",
                Website = "www.brokeprotocol.com" // huh?
            };
        }
    }
}
