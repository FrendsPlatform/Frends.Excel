using System;
using System.Threading;
using NUnit.Framework;

namespace Frends.Excel.CreateFromCsv.Tests;

[TestFixture]
public class ErrorHandlerTest : TestBase
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    [SetUp]
    public void Setup()
    {
        Input = DefaultInput();
        Input.SourcePath = "C:/invalid path";
        Options = DefaultOptions();
    }

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var ex = Assert.Throws<Exception>(() =>
            Excel.CreateFromCsv(Input, Options, CancellationToken.None));
        Assert.That(ex, Is.Not.Null);
    }

    [Test]
    public void Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        Options.ThrowErrorOnFailure = false;
        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        Options.ErrorMessageOnFailure = CustomErrorMessage;
        var ex = Assert.Throws<Exception>(() =>
            Excel.CreateFromCsv(Input, Options, CancellationToken.None));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Contains.Substring(CustomErrorMessage));
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
}
