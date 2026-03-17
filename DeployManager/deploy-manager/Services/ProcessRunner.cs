using DeployManager.Models;
using System.Diagnostics;

namespace DeployManager.Services;

public class ProcessRunner
{
    public ProcessResult Run(string fileName, string arguments)
    {
        try
        {
            var process = new Process();

            process.StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            return new ProcessResult
            {
                ExitCode = process.ExitCode,
                Output = output,
                Error = error
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro executando {fileName}: {ex.Message}", ex);
        }
    }
}
