using System;

namespace Frends.Excel.Parse.Definitions;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// Summary of the error.
    /// </summary>
    /// <example>Unable to parse the workbook.</example>
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    /// <example>object { Message = "The file could not be found." }</example>
    public Exception? AdditionalInfo { get; init; }
}
