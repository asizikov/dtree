using System.Diagnostics;

namespace dtree.IntegrationTests;

public class CliRunner
{
    private readonly string cliProjectPath;
    private readonly Process process;

    public CliRunner(string cliProjectPath)
    {
        this.cliProjectPath = cliProjectPath;

        process = new Process();
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
    }

    public (int exitCode, string output) Run(string args)
    {
        process.StartInfo.Arguments = $"run --project {cliProjectPath} -- {args}";

        process.Start();
        var output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        return (process.ExitCode, output);
    }
}