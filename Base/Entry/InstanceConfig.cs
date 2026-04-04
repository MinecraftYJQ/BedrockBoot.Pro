using System.Text.Json.Serialization;
using Round.SDK.Entity;

namespace Plugin.Pro.Base.Entry;

public class InstanceConfig
{
    [JsonPropertyName("isUnlock")] public bool IsUnlock { get; set; } = false;

    public static ConfigEntity<InstanceConfig> GetInstanceConfig(string path)
    {
        var confFile = Path.Combine(path, "config", "bedrockboot2", "bb.pro", "config.json");
        var conf = new ConfigEntity<InstanceConfig>(confFile);

        return conf;
    }
}