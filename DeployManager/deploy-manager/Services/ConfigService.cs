
using System.Text.Json;
using DeployManager.Models;

namespace DeployManager.Services;

public class ConfigService
{
    public DeployConfig Load(string path = "apps.json")
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<DeployConfig>(json)!;
    }
}
