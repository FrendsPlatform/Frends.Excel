using System.IO;
using System.Threading;
using NUnit.Framework;

namespace Frends.Excel.CreateFromCsv.Tests;

public class ValidatorTests : TestBase
{
    [SetUp]
    public void Setup()
    {
        Input = DefaultInput();
        Options = DefaultOptions();
        Options.ThrowErrorOnFailure = false;
        if (Directory.Exists(DestinationDirectoryPath)) Directory.Delete(DestinationDirectoryPath, true);
    }

    [Test]
    public void ValidationsShouldReturnCorrectMessages()
    {
        Input.SourcePath = "nonExistingFile.csv";
        Input.SheetName = string.Empty;
        Input.Delimiter = string.Empty;
        Input.DestinationFileName = string.Empty;
        Input.DestinationDirectory = string.Empty;
        Options.ThrowErrorOnFailure = false;

        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error.Message, Contains.Substring("SourcePath must be a valid, existing file path."));
        Assert.That(result.Error.Message, Contains.Substring("SheetName is required and cannot be empty."));
        Assert.That(result.Error.Message, Contains.Substring("Delimiter is required and cannot be empty."));
        Assert.That(result.Error.Message, Contains.Substring("DestinationFileName is required and cannot be empty."));
        Assert.That(result.Error.Message, Contains.Substring("DestinationDirectory is required and cannot be empty."));
    }

    [TestCase("simple.csv", "result")]
    [TestCase("simple.csv", "result.xlsx")]
    public void ValidateExtensionWithSuccess(string srcName, string dstName)
    {
        Input.SourcePath = Path.Combine(WorkingDirectory, srcName);
        Input.DestinationFileName = dstName;

        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
    }

    [TestCase("simple", "result")]
    [TestCase("", "result")]
    [TestCase("simple.csv", "result.pdf")]
    public void ValidateExtensionWithFailure(string srcName, string dstName)
    {
        Input.SourcePath = Path.Combine(WorkingDirectory, srcName);
        Input.DestinationFileName = dstName;

        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error.Message, Contains.Substring("has an invalid extension."));
    }
}
