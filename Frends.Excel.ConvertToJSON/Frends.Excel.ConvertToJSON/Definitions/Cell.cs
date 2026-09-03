namespace Frends.Excel.ConvertToJSON.Definitions;

internal class Cell
{
    public Cell()
    {
    }

    /// <summary>
    /// Column name in letter or number format.
    /// </summary>
    /// <example>A</example>
    public dynamic? ColumnName { get; set; }

    /// <summary>
    /// Column index in the worksheet.
    /// </summary>
    /// <example>1</example>
    public int ColumnIndex { get; set; }

    /// <summary>
    /// Cell value as text.
    /// </summary>
    /// <example>Foo</example>
    public string? ColumnValue { get; set; }
}
