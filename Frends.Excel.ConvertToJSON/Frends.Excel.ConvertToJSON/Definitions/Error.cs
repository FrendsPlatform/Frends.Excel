using System;

namespace Frends.Excel.ConvertToJSON.Definitions;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// Summary of the error.
    /// </summary>
    /// <example>Unable to convert Excel file to JSON.</example>
    public string? Message { get; init; }

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    /// <example>object { Exception AdditionalInfo }</example>
    public Exception? AdditionalInfo { get; set; }
}
