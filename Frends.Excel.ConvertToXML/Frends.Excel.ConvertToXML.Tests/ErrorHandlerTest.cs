using System;
using System.IO;
using System.Threading;
using Frends.Excel.ConvertToXML.Definitions;
using NUnit.Framework;

namespace Frends.Excel.ConvertToXML.Tests;

[TestFixture]
public class ErrorHandlerTest
{
    private const string CustomErrorMessage = "CustomErrorMessage";
    private Input _input = null!;
    private Options _options = null!;

    [SetUp]
    public void Setup()
    {
        var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../../../TestData/");
        _input = new Input { Path = Path.Combine(basePath, "file-does-not-exist.xlsx") };
        _options = new Options();
    }

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        Assert.That(() => Excel.ConvertToXML(_input, _options, CancellationToken.None), Throws.Exception);
    }

    [Test]
    public void Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        _options.ThrowErrorOnFailure = false;
        var result = Excel.ConvertToXML(_input, _options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
        Assert.That(result.Error!.Message, Does.Contain("file-does-not-exist.xlsx"));
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        _options.ErrorMessageOnFailure = CustomErrorMessage;
        var ex = Assert.Throws<Exception>(() =>
            Excel.ConvertToXML(_input, _options, CancellationToken.None));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Contains.Substring(CustomErrorMessage));
    }
}
