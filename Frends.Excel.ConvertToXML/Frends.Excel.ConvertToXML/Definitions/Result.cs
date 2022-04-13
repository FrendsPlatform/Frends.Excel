using System.ComponentModel;

namespace Frends.Excel.ConvertToXML.Definitions;

/// <summary>
/// ConvertToXML task result.
/// </summary>
public class Result
{
    /// <summary>
    /// False if conversion fails.
    /// </summary>
    [DefaultValue("false")]
    public bool Success { get; set; }

    /// <summary>
    /// Exception message, if any. Note that ThrowErrorOnFailure
    /// should be false to get errors as part of result.
    /// </summary>
    [DefaultValue("")]
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// Excel-conversion to XML.
    /// </summary>
    public string? XML { get; private set; }

    internal Result(bool success, string? xml, string? errorMessage)
    {
        Success = success;
        XML = xml;
        ErrorMessage = errorMessage;
    }
}
