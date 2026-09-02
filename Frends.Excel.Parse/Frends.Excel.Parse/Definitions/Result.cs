using System.ComponentModel;

namespace Frends.Excel.Parse.Definitions;

/// <summary>
/// Result of the task.
/// </summary>
public class Result
{
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
    public Error? Error { get; init; }

    /// <summary>
    /// Parsed workbook contents.
    /// </summary>
    /// <example>object { Tables = [object { TableName = "Sheet1", Columns = ["Column0"], Rows = [["Value"]] }] }</example>
    public WorkbookData? DataSet { get; init; }

    /// <summary>
    /// Initializes a new task result.
    /// </summary>
    /// <param name="success">Whether the task completed successfully.</param>
    /// <param name="dataSet">Parsed workbook data.</param>
    /// <param name="error">Error details when the task fails.</param>
    public Result(bool success, WorkbookData? dataSet = null, Error? error = null)
    {
        Success = success;
        DataSet = dataSet;
        Error = error;
    }
}
