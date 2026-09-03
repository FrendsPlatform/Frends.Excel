using Frends.Excel.ConvertToCSV.Definitions;
using NUnit.Framework;
using System;
using System.Threading;

namespace Frends.Excel.ConvertToCSV.Tests;

[TestFixture]
internal class ErrorHandlerTest
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var ex = Assert.Throws<Exception>(() =>
            Excel.ConvertToCSV(DefaultInput(), DefaultOptions(), CancellationToken.None));

        Assert.That(ex, Is.Not.Null);
    }

    [Test]
    public void Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = DefaultOptions();
        options.ThrowErrorOnFailure = false;
        var result = Excel.ConvertToCSV(DefaultInput(), options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = DefaultOptions();
        options.ErrorMessageOnFailure = CustomErrorMessage;

        var ex = Assert.Throws<Exception>(() =>
            Excel.ConvertToCSV(DefaultInput(), options, CancellationToken.None));

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex?.Message, Does.Contain(CustomErrorMessage));
    }

    private Input DefaultInput()
    {
        return new Input();
    }

    private Options DefaultOptions()
    {
        return new Options();
    }
}