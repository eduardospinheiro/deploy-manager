
using DeployManager.Models;
using DeployManager.Services;

namespace DeployManager.Deploy;

public class DeployOrchestrator
{
    private readonly GitHubService _github;
    private readonly GitService _git;
    private readonly BuildService _build;

    public DeployOrchestrator(GitHubService github, GitService git, BuildService build)
    {
        _github = github;
        _git = git;
        _build = build;
    }

    public async Task Deploy(AppConfig app, string baseRepoPath, string branch)
    {
        var repoPath = Path.Combine(baseRepoPath, app.Name);
        var repoUrl = $"https://github.com/{app.Owner}/{app.Repo}.git";

        if (!Directory.Exists(repoPath))
            _git.Clone(repoUrl, repoPath);

        _git.Checkout(repoPath, branch);

        var solutionPath = Path.Combine(repoPath, app.Solution);

        _build.Build(solutionPath, app.PublishPath);

        var branches = await _github.GetBranches(app.Owner, app.Repo);

        var selected = branches.First(b => b.Name == branch);

        var tagName = $"{app.Name}-{DateTime.Now:yyyyMMddHHmm}";

        await _github.CreateTag(app.Owner, app.Repo, tagName, selected.Commit.Sha);

        Console.WriteLine($"Deploy concluído. Tag criada: {tagName}");
    }
}
