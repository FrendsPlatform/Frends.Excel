using System;
using System.IO;
using System.Threading;
using Frends.Excel.Parse.Definitions;
using NUnit.Framework;

namespace Frends.Excel.Parse.Tests;

[TestFixture]
public class ExcelConvertTests
{
    private readonly Input _input = new();
    private readonly Options _options = new();

    // Cat image in example files is from Pixbay.com. It is licenced in CC0 Public Domain (Free for commercial use, No attribution required).
    // It is uploaded by Ben_Kerckx https://pixabay.com/en/cat-animal-pet-cats-close-up-300572/


    [SetUp]
    public void Setup()
    {
        _input.Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../../../TestData/");
    }

    [Test]
    public void TestParse()
    {
        _input.Path = Path.Combine(_input.Path, "ExcelTestInput2.xls");
        var result = Excel.Parse(_input, _options, new CancellationToken());
        Assert.That(result.Success, Is.True);
        Assert.That(result.Error, Is.Null);
        Assert.That(result.DataSet, Is.Not.Null);
        var dataSet = result.DataSet!;
        Assert.AreEqual(2, dataSet.Tables.Count);
        Assert.AreEqual("Sheet1", dataSet.Tables[0].TableName);
        Assert.AreEqual("OmituinenNimi", dataSet.Tables[1].TableName);
    }

    [Test]
    public void ShouldThrowUnknownFileFormatError()
    {
        _input.Path = Path.Combine(_input.Path, "UnitTestErrorFile.txt");
        _options.ThrowErrorOnFailure = true;
        Assert.That(() => Excel.Parse(_input, _options, new CancellationToken()), Throws.Exception);
    }

    [Test]
    public void DoNotThrowOnFailure()
    {
        _input.Path = Path.Combine(_input.Path, "thisfiledoesnotexist.txt");
        _options.ThrowErrorOnFailure = false;
        var result = Excel.Parse(_input, _options, new CancellationToken());
        Assert.That(result.Success, Is.False);
        Assert.That(result.DataSet, Is.Null);
        Assert.That(result.Error, Is.Not.Null);
    }
}