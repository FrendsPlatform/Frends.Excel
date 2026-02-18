using System.IO;
using System.Threading;
using ClosedXML.Excel;
using Frends.Excel.CreateFromCsv.Definitions;
using NUnit.Framework;

namespace Frends.Excel.CreateFromCsv.Tests;

[TestFixture]
public class FunctionalTests : TestBase
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
    public void ShouldWriteSimpleCsvToExcel()
    {
        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
        Assert.That(File.Exists(ResultFilePath), Is.True, $"File was not created: {ResultFilePath}");
        Assert.That(result.OutputPath, Is.EqualTo(ResultFilePath));
    }

    [Test]
    public void ShouldFailWhenFileAlreadyExists()
    {
        Directory.CreateDirectory(DestinationDirectoryPath);
        File.WriteAllText(ResultFilePath, "Some content");
        Options.FileExistAction = FileExistAction.Throw;

        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error.Message, Contains.Substring("already exists"));
        var content = File.ReadAllText(ResultFilePath);
        Assert.That(content, Is.EqualTo("Some content"));
    }

    [Test]
    public void ShouldOverwriteWhenFileAlreadyExists()
    {
        Directory.CreateDirectory(DestinationDirectoryPath);
        File.WriteAllText(ResultFilePath, "Some content");
        Options.FileExistAction = FileExistAction.Overwrite;

        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
        Assert.That(File.Exists(ResultFilePath), Is.True, $"File was not created: {ResultFilePath}");
        var content = File.ReadAllText(ResultFilePath);
        Assert.That(content, Is.Not.EqualTo("Some content"));
        Assert.That(result.OutputPath, Is.EqualTo(ResultFilePath));
    }

    [Test]
    public void ShouldRenameWhenFileAlreadyExists()
    {
        Directory.CreateDirectory(DestinationDirectoryPath);
        File.WriteAllText(ResultFilePath, "Some content");
        var firstCopyPath = Path.Combine(DestinationDirectoryPath, $"{ResultFileName} (1).xlsx");
        var secondCopyPath = Path.Combine(DestinationDirectoryPath, $"{ResultFileName} (2).xlsx");
        File.WriteAllText(firstCopyPath, "Some content");
        Options.FileExistAction = FileExistAction.Rename;

        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
        Assert.That(result.OutputPath, Is.EqualTo(secondCopyPath));
        Assert.That(File.Exists(secondCopyPath), Is.True, $"File was not created: {secondCopyPath}");
    }

    [Test]
    public void ShouldTrimValues()
    {
        Input.SourcePath = Path.Combine(WorkingDirectory, "values_with_space.csv");
        Options.TrimValues = true;

        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        var val = GetCellValue(ResultFilePath, 2, 2);
        Assert.That(result.Success, Is.True);
        Assert.That(val, Is.EqualTo("John Doe"));
    }

    [Test]
    public void ShouldSkipRowsFromTop()
    {
        Input.SourcePath = Path.Combine(WorkingDirectory, "start_comment.csv");
        Options.SkipRowsFromTop = 1;

        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        var val = GetCellValue(ResultFilePath, 1, 1);
        Assert.That(result.Success, Is.True);
        Assert.That(val, Is.EqualTo("id"));
    }

    [Test]
    public void ShouldIgnoreQuotes()
    {
        Input.SourcePath = Path.Combine(WorkingDirectory, "ignored_quotes.csv");
        Options.IgnoreQuotes = true;

        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        var val = GetCellValue(ResultFilePath, 1, 2);
        Assert.That(result.Success, Is.True);
        Assert.That(val, Is.EqualTo("\"name"));
    }

    [Test]
    public void ShouldRespectValueTypes()
    {
        var result = Excel.CreateFromCsv(Input, Options, CancellationToken.None);

        using var workbook = new XLWorkbook(ResultFilePath);
        var worksheet = workbook.Worksheet(1);
        var cell = worksheet.Cell(2, 3);
        var number = cell.GetValue<double>();
        Assert.That(result.Success, Is.True);
        Assert.That(number, Is.EqualTo(25));
    }

    [TearDown]
    public void Teardown()
    {
        if (Directory.Exists(DestinationDirectoryPath)) Directory.Delete(DestinationDirectoryPath, true);
    }

    private static string GetCellValue(string filePath, int row, int column)
    {
        using var workbook = new XLWorkbook(filePath);
        var worksheet = workbook.Worksheet(1);
        var cell = worksheet.Cell(row, column);

        return cell.GetValue<string>();
    }
}
