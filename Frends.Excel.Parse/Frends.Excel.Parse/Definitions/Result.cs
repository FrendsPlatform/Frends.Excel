namespace Frends.Excel.Parse.Definitions;

using System.ComponentModel;
using System.Data;

/// <summary>
/// Result of the task.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="success">Indicates whether the operation completed successfully.</param>
    /// <param name="dataSet">Contains the returned data set.</param>
    /// <param name="error">Contains error details when the operation fails.</param>
    public Result(bool success, DataSet dataSet, Error error)
    {
        Success = success;
        DataSet = dataSet;
        Error = error;
    }

    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool Success { get; init; }

    /// <summary>
    /// Error details. Null when Success is true.
    /// </summary>
    /// <example>null</example>
    public Error Error { get; init; }

    /// <summary>
    /// Parsed workbook contents.
    /// </summary>
    /// <example>object { Tables = [object { TableName = "Sheet1", Columns = ["Column0"], Rows = [["Value"]] }] }</example>
#pragma warning disable FT0017 // Intentionally suppress this analyzer rule: DataSet is provided by .NET and is required to represent the parsed Excel workbook
    public DataSet DataSet { get; internal set; }
#pragma warning restore FT0017
}
