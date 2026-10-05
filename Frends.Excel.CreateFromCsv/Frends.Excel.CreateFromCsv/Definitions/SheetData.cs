using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Frends.Excel.CreateFromCsv.Helpers;

namespace Frends.Excel.CreateFromCsv.Definitions;

/// <summary>
/// Additional sheet data
/// </summary>
public class SheetData
{
    /// <summary>
    /// Path to the .csv file.
    /// </summary>
    /// <example>C:/workdir/data.csv</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    [NotEmptyString]
    [ExistingFilePath]
    [Required]
    [ValidExtension(["csv"])]
    public string SourcePath { get; set; } = string.Empty;

    /// <summary>
    /// Name of the sheet to write to.
    /// </summary>
    /// <example>FirstSheet</example>
    [DefaultValue("")]
    [NotEmptyString]
    public string SheetName { get; set; } = string.Empty;

    /// <summary>
    /// Delimiter.
    /// </summary>
    /// <example>;</example>
    [DefaultValue(";")]
    [NotEmptyString]
    public string Delimiter { get; set; }
}
