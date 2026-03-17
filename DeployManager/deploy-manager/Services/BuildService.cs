
using DeployManager.Exceptions;
using System.Diagnostics;

namespace DeployManager.Services;

public class BuildService
{
    private readonly ProcessRunner _runner = new();
    public void Build(string solution, string publishPath)
    {
        var result = _runner.Run(
            "dotnet",
            $"publish {solution} -c Release -o {publishPath}"
        );

        Console.WriteLine(result.Output);

        if (result.ExitCode != 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();

            throw new DeployException(
                $"Erro no build. ExitCode: {result.ExitCode}"
            );
        }
    }
}
