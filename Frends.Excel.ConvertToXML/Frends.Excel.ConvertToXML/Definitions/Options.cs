using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Excel.ConvertToXML.Definitions;

/// <summary>
/// Task options.
/// </summary>
public class Options
{
    /// <summary>
    /// If specified - only the specified worksheet will be read.
    /// If empty, all work sheets are read.
    /// </summary>
    [DefaultValue(@"")]
    public string? ReadOnlyWorkSheetWithName { get; set; }

    /// <summary>
    /// If set to true, numbers will be used as column headers instead of letters (A = 1, B = 2...).
    /// </summary>
    [DefaultValue("false")]
    public bool UseNumbersAsColumnHeaders { get; set; }

    /// <summary>
    /// Choose if exception should be thrown when conversion fails.
    /// </summary>
    [DefaultValue("true")]
    public bool ThrowErrorOnFailure { get; set; }

    /// <summary>
    /// Date format selection.
    /// </summary>
    [DisplayName("Date Format")]
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue(DateFormats.DEFAULT)]
    public DateFormats DateFormat { get; set; }

    /// <summary>
    /// If set to true, dates will exclude timestamps from dates.
    /// </summary>
    [DefaultValue("false")]
    public bool ShortDatePattern { get; set; }

    internal bool ShouldReadWorkSheet(string worksheetName)
    {
        // Option to read only one sheet is not set, thus we should read any worksheet
        if (string.IsNullOrWhiteSpace(this.ReadOnlyWorkSheetWithName)) return true;

        // Read worksheet if its name matches the option
        return this.ReadOnlyWorkSheetWithName == worksheetName;
    }
}
