
using DeployManager.Exceptions;
using Octokit;
using System.Diagnostics;

namespace DeployManager.Services;

public class GitService
{
    private readonly ProcessRunner _runner = new();
    public void Clone(string repoUrl, string path)
    {
        var result = _runner.Run("git", $"clone {repoUrl} {path}");

        if (result.ExitCode != 0)
            throw new DeployException(result.Error);
    }

    public void Checkout(string repoPath, string branch)
    {
        var result = _runner.Run("git", $"-C {repoPath} checkout {branch}");

        if (result.ExitCode != 0)
            throw new DeployException(result.Error);
    }
}
