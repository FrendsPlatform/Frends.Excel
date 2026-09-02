using System;
using System.IO;
using System.Threading;
using Frends.Excel.Parse.Definitions;
using NUnit.Framework;

namespace Frends.Excel.Parse.Tests;

[TestFixture]
public class ErrorHandlerTest
{
    private const string CustomErrorMessage = "CustomErrorMessage";
    private readonly Input _input = new();
    private readonly Options _options = new();

    [SetUp]
    public void Setup()
    {
        _input.Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../TestData/thisfiledoesnotexist.txt");
        _options.ThrowErrorOnFailure = true;
        _options.ErrorMessageOnFailure = string.Empty;
    }

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        Assert.That(
            () => Excel.Parse(_input, _options, CancellationToken.None),
            Throws.Exception);
    }

    [Test]
    public void Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        _options.ThrowErrorOnFailure = false;
        var result = Excel.Parse(_input, _options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        _options.ErrorMessageOnFailure = CustomErrorMessage;
        var ex = Assert.Throws<Exception>(() =>
            Excel.Parse(_input, _options, CancellationToken.None));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Contains.Substring(CustomErrorMessage));
    }
}
