using System;
using System.IO;
using Frends.Excel.CreateFromCsv.Definitions;

namespace Frends.Excel.CreateFromCsv.Tests;

public abstract class TestBase
{
    protected const string ResultFileName = "result";
    protected static readonly string DestinationDirectoryPath = Path.Combine(WorkingDirectory, "results");
    protected static readonly string ResultFilePath = Path.Combine(DestinationDirectoryPath, "result.xlsx");

    protected static string WorkingDirectory => Path.Combine(Environment.CurrentDirectory, "TestData");

    protected Input Input { get; set; }

    protected Options Options { get; set; }

    protected static Input DefaultInput() => new()
    {
        SourcePath = Path.Combine(WorkingDirectory, "simple.csv"),
        DestinationFileName = ResultFileName,
        SheetName = "data",
        Delimiter = ";",
        DestinationDirectory = DestinationDirectoryPath,
    };

    protected static Options DefaultOptions() => new();
}
