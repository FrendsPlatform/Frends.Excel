using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Excel.ConvertToXML.Definitions;

public class Input
{
    /// <summary>
    /// Path to the Excel file.
    /// </summary>
    [DefaultValue(@"C:\tmp\ExcelFile.xlsx")]
    [DisplayFormat(DataFormatString = "Text")]
    public string Path { get; set; }
}
