using System.Diagnostics;

namespace DashReport.Specs.Drivers;

public class CliDriver
{
    public void Run(string[] arguments)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "dashreport",
            Arguments = string.Join(" ", arguments),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process
        {
            StartInfo = processStartInfo
        };

        process.Start();
        process.WaitForExit();

        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();

        if (process.ExitCode != 0)
        {
            throw new Exception($"CLI execution failed with exit code {process.ExitCode}: {error}");
        }
    }
}
