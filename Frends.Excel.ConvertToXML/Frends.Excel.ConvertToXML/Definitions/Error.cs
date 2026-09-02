using System;

namespace Frends.Excel.ConvertToXML.Definitions;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// Summary of the error.
    /// </summary>
    /// <example>Error while converting Excel file to XML.</example>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    /// <example>object { Exception AdditionalInfo }</example>
    public Exception AdditionalInfo { get; set; } = null!;
}
