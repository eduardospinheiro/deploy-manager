
using System.Diagnostics;

namespace DeployManager.Services;

public class GitService
{
    public void Clone(string repoUrl, string path)
    {
        Run("git", $"clone {repoUrl} {path}");
    }

    public void Checkout(string repoPath, string branch)
    {
        Run("git", $"-C {repoPath} checkout {branch}");
        Run("git", $"-C {repoPath} pull origin {branch}");
    }

    private void Run(string file, string args)
    {
        var p = new Process();

        p.StartInfo.FileName = file;
        p.StartInfo.Arguments = args;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.UseShellExecute = false;

        p.Start();

        Console.WriteLine(p.StandardOutput.ReadToEnd());
        Console.WriteLine(p.StandardError.ReadToEnd());

        p.WaitForExit();
    }
}
