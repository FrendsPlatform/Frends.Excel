using Frends.Excel.CreateFromCsv.Definitions;
using System;
using System.IO;

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
        SheetName = "Sheet1",
        Delimiter = ";",
        DestinationDirectory = DestinationDirectoryPath,
        AdditionalSheets = new SheetData[]
        {
            new SheetData
            {
                SheetName = "Sheet2",
                Delimiter = ";",
                SourcePath = Path.Combine(WorkingDirectory, "simple2.csv"),
            },
        },
    };

    protected static Options DefaultOptions() => new();
}
