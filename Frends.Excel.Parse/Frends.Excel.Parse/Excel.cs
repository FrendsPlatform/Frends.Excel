using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using ExcelDataReader;
using Frends.Excel.Parse.Definitions;
using Frends.Excel.Parse.Helpers;

namespace Frends.Excel.Parse;

/// <summary>
/// Task for parsing Excel files.
/// </summary>
public static class Excel
{
    /// <summary>
    /// Converts Excel file to data set. [Documentation](https://tasks.frends.com/tasks#frends-tasks/Frends.Excel.Parse)
    /// </summary>
    /// <param name="input">Input configuration</param>
    /// <param name="options">Input options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result containing the parsed Excel workbook data.</returns>
    /// <exception cref="Exception"></exception>
    public static Result Parse(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        options ??= new Options();

        try
        {
            ValidationHandler.Run(input, options);
            cancellationToken.ThrowIfCancellationRequested();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var stream = new FileStream(input.Path, FileMode.Open, FileAccess.Read);
            using var excelReader = ExcelReaderFactory.CreateReader(stream);
            var result = ConvertToWorkbookData(excelReader.AsDataSet(), cancellationToken);
            return new Result(true, result);
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }

    private static WorkbookData ConvertToWorkbookData(DataSet dataSet, CancellationToken cancellationToken)
    {
        return new WorkbookData
        {
            Tables = dataSet.Tables
                .Cast<DataTable>()
                .Select(table =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return new WorksheetData
                    {
                        TableName = table.TableName,
                        Columns = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList(),
                        Rows = table.Rows.Cast<DataRow>()
                            .Select(row => row.ItemArray.Select(value => value == DBNull.Value ? null : value).ToList())
                            .ToList(),
                    };
                })
                .ToList(),
        };
    }
}
