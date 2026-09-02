namespace Frends.Excel.ConvertToJSON.Definitions;

internal class Row
{
    /// <summary>
    /// Row number in the worksheet.
    /// </summary>
    /// <example>1</example>
    public int RowNumber { get; set; }

    /// <summary>
    /// Cells found on this row.
    /// </summary>
    /// <example>List of cells with column metadata and values</example>
    public List<Cell>? Cells { get; set; }

    public Row() { }
}
