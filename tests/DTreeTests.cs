using System.Diagnostics;
using System.IO;
using Xunit.Abstractions;

namespace dtree.IntegrationTests;

public class DTreeTests
{
    private readonly string cliProjectPath;
    private readonly Process process;

    public DTreeTests(ITestOutputHelper output)
    {
        cliProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../../src/dtree.csproj");
        output.WriteLine($"{cliProjectPath}");

        process = new Process();
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
    }

    [Theory]
    [InlineData("-d")]
    [InlineData("--max-depth")]
    public void Negative_Depth_Results_As_Error(string key)
    {
        process.StartInfo.Arguments = $"run --project {cliProjectPath} -- {key} -1";

        process.Start();
        var output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        Assert.Equal($"😱 Max depth must be greater than 0{Environment.NewLine}", output);
        Assert.Equal(1, process.ExitCode);
    }
}