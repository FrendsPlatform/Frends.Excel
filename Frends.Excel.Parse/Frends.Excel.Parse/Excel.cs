using System.ComponentModel;
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
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>Result containing the parsed Excel: object { bool Success, string ErrorMessage, DataSet DataSet }</returns>
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

            using (var stream = new FileStream(input.Path, FileMode.Open, FileAccess.Read))
            {
                using (var excelReader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = excelReader.AsDataSet();
                    return new Result(true, result, null);
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }
}
