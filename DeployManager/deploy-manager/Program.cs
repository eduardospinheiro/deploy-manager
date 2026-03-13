
using DeployManager.Services;
using DeployManager.Deploy;

var configService = new ConfigService();
var config = configService.Load();

var github = new GitHubService(config.GithubToken);
var git = new GitService();
var build = new BuildService();

var orchestrator = new DeployOrchestrator(github, git, build);

Console.WriteLine("Aplicações disponíveis:");

for (int i = 0; i < config.Apps.Count; i++)
{
    Console.WriteLine($"{i+1} - {config.Apps[i].Name}");
}

var appIndex = int.Parse(Console.ReadLine()!) - 1;

var app = config.Apps[appIndex];

Console.WriteLine($"Branch (default {app.DefaultBranch}):");

var branch = Console.ReadLine();

if (string.IsNullOrWhiteSpace(branch))
    branch = app.DefaultBranch;

await orchestrator.Deploy(app, config.ReposBasePath, branch);
