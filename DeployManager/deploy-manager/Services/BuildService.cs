
using System.Diagnostics;

namespace DeployManager.Services;

public class BuildService
{
    public void Build(string solution, string publishPath)
    {
        var p = new Process();

        p.StartInfo.FileName = "dotnet";
        p.StartInfo.Arguments = $"publish {solution} -c Release -o {publishPath}";
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.UseShellExecute = false;

        p.Start();

        Console.WriteLine(p.StandardOutput.ReadToEnd());
        Console.WriteLine(p.StandardError.ReadToEnd());

        p.WaitForExit();
    }
}
