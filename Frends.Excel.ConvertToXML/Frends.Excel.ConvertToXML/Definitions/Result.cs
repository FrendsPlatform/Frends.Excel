using System.ComponentModel;

namespace Frends.Excel.ConvertToXML.Definitions;

/// <summary>
/// Result.
/// </summary>
public class Result
{
    internal Result(bool success, string xml, Error error = null)
    {
        Success = success;
        XML = xml;
        Error = error;
    }

    /// <summary>
    /// Conversion's status. False if conversion fails.
    /// </summary>
    /// <example>true</example>
    [DefaultValue("false")]
    public bool Success { get; set; }

    /// <summary>
    /// Excel-conversion to CSV.
    /// </summary>
    /// <example>workbook_name, worksheet_name, row_header, column_header</example>
    public string XML { get; private set; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; private set; }
}