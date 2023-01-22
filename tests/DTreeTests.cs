using Xunit.Abstractions;

namespace dtree.IntegrationTests;

public class DTreeTests
{
    private readonly string cliProjectPath;
    private readonly CliRunner runner;

    public DTreeTests(ITestOutputHelper output)
    {
        cliProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../../src/dtree.csproj");
        output.WriteLine($"{cliProjectPath}");
        runner = new CliRunner(cliProjectPath);
    }

    [Theory]
    [InlineData("-d")]
    [InlineData("--max-depth")]
    public void Negative_Depth_Results_As_Error(string key)
    {
        var (exitCode, output) = runner.Run($"{key} -1");

        Assert.Equal($"😱 Max depth must be greater than 0{Environment.NewLine}", output);
        Assert.Equal(1, exitCode);
    }

    [Theory]
    [InlineData("-a")]
    [InlineData("--all")]
    public void All_Files_Should_Be_Included(string key)
    {
       var (exitCode, output) = runner.Run($"{key} -p ../../../../");

        Assert.Contains("📁 .devcontainer", output);
        Assert.Contains("📁 .github", output);
        Assert.Contains("📁 src", output);
        Assert.Contains(".gitignore", output);
        Assert.Contains("LICENSE", output);
        Assert.Contains("README.md", output);
        Assert.Equal(0, exitCode);
    }
}