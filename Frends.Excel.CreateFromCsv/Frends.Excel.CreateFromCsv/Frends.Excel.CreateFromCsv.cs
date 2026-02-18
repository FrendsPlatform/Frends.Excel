using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Threading;
using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using Frends.Excel.CreateFromCsv.Definitions;
using Frends.Excel.CreateFromCsv.Helpers;

namespace Frends.Excel.CreateFromCsv;

/// <summary>
/// Task Class for Excel operations.
/// </summary>
public static class Excel
{
    /// <summary>
    /// Task to create an .xlsx file from data stored in .csv file
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends-Excel-CreateFromCsv)
    /// </summary>
    /// <param name="input">Essential parameters.</param>
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, string OutputPath, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static Result CreateFromCsv(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        string tempPath = Path.ChangeExtension(Path.GetTempPath(), $"{Guid.NewGuid()}.xlsx");

        try
        {
            var validationMessage = ValidationHandler.Validate(input);

            if (validationMessage != string.Empty) throw new Exception($"Validation failed:\n{validationMessage}");
            if (!Directory.Exists(input.DestinationDirectory)) Directory.CreateDirectory(input.DestinationDirectory);

            string outputPath = Path.Combine(input.DestinationDirectory, input.DestinationFileName);
            outputPath = Path.ChangeExtension(outputPath, ".xlsx");

            if (File.Exists(outputPath))
            {
                switch (options.FileExistAction)
                {
                    case FileExistAction.Throw:
                        throw new Exception($"File {outputPath} already exists.");
                    case FileExistAction.Overwrite:
                        break;
                    case FileExistAction.Rename:
                        outputPath = FileHandler.Rename(outputPath);

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(
                            nameof(options),
                            options.FileExistAction,
                            "Action not supported.");
                }
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(input.SheetName);

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = options.ContainsHeaderRow,
                Delimiter = input.Delimiter,
                TrimOptions = options.TrimValues ? TrimOptions.Trim : TrimOptions.None,
                IgnoreBlankLines = options.SkipEmptyRows,
                Mode = options.IgnoreQuotes ? CsvMode.NoEscape : CsvMode.RFC4180,
            };

            using StreamReader sr = new StreamReader(input.SourcePath);

            for (var i = 0; i < options.SkipRowsFromTop; i++) _ = sr.ReadLine();

            using var csvReader = new CsvReader(sr, configuration);

            var rowCounter = 0;

            while (csvReader.Read())
            {
                rowCounter++;

                for (var index = 0; index < csvReader.ColumnCount; index++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string rawValue = csvReader.GetField(index);
                    object typedValue = FileHandler.ParseValue(rawValue, configuration.CultureInfo);
                    worksheet.Cell(rowCounter, index + 1).Value = XLCellValue.FromObject(typedValue);
                }
            }

            if (options.ContainsHeaderRow && rowCounter > 0)
            {
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                worksheet.SheetView.FreezeRows(1);
            }

            if (options.AdjustColumnsToContents) worksheet.Columns().AdjustToContents();
            workbook.SaveAs(tempPath);

            if (File.Exists(outputPath)) File.Delete(outputPath);
            File.Move(tempPath, outputPath!);

            return new Result
            {
                OutputPath = outputPath,
            };
        }
        catch (Exception ex)
        {
            if (File.Exists(tempPath)) File.Delete(tempPath);

            return ErrorHandler.Handle(ex, options.ThrowErrorOnFailure, options.ErrorMessageOnFailure);
        }
    }
}
