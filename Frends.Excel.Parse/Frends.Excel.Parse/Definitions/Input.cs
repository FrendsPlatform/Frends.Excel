using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Excel.Parse.Definitions;

/// <summary>
/// Input parameters for the task.
/// </summary>
public class Input
{
    /// <summary>
    /// Path to the Excel file.
    /// </summary>
    /// <example>C:\temp\Workbook.xlsx</example>
    [DefaultValue(@"C:\tmp\ExcelFile.xlsx")]
    [Required]
    [DisplayFormat(DataFormatString = "Text")]
    public string Path { get; set; } = string.Empty;
}
