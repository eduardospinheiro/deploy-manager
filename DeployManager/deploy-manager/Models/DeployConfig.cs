
namespace DeployManager.Models;

public class DeployConfig
{
    public string GithubToken { get; set; } = "";
    public string ReposBasePath { get; set; } = "";
    public List<AppConfig> Apps { get; set; } = new();
}

public class AppConfig
{
    public string Name { get; set; } = "";
    public string Owner { get; set; } = "";
    public string Repo { get; set; } = "";
    public string DefaultBranch { get; set; } = "";
    public string Solution { get; set; } = "";
    public string PublishPath { get; set; } = "";
}
