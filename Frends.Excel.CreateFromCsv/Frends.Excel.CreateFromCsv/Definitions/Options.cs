using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Excel.CreateFromCsv.Definitions;

/// <summary>
/// Additional parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// This flag tells the reader if there is a header row in the CSV string.
    /// </summary>
    /// <example>true</example>
    [DefaultValue("true")]
    public bool ContainsHeaderRow { get; set; } = true;

    /// <summary>
    /// This flag tells the reader to trim whitespace from the beginning and ending of the field value when reading.
    /// </summary>
    /// <example>true</example>
    [DefaultValue("true")]
    public bool TrimValues { get; set; } = true;

    /// <summary>
    /// If the CSV string contains metadata before the header row,
    /// you can set this value to ignore a specific number of rows from the beginning of the csv string.
    /// </summary>
    /// <example>2</example>
    [DefaultValue(0)]
    public int SkipRowsFromTop { get; set; }

    /// <summary>
    /// A flag to let the reader know if a record should be skipped when reading if it's empty.
    /// A record is considered empty if all fields are empty.
    /// </summary>
    /// <example>false</example>
    [DefaultValue("false")]
    public bool SkipEmptyRows { get; set; } = false;

    /// <summary>
    /// A flag to let the reader know if quotes should be ignored.
    /// </summary>
    /// <example>false</example>
    [DefaultValue("false")]
    public bool IgnoreQuotes { get; set; }

    /// <summary>
    /// What to do if the output file already exists.
    /// </summary>
    /// <example>Throw</example>
    public FileExistAction FileExistAction { get; set; } = FileExistAction.Throw;

    /// <summary>
    /// Whether to throw an error on failure.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Overrides the error message on failure.
    /// </summary>
    /// <example>Custom error message</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
