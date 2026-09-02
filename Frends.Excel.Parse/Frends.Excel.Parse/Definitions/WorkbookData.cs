using System.Collections.Generic;

namespace Frends.Excel.Parse.Definitions;

/// <summary>
/// Parsed workbook data.
/// </summary>
public class WorkbookData
{
    /// <summary>
    /// Worksheets parsed from the workbook.
    /// </summary>
    /// <example>object { Tables = [object { TableName = "Sheet1", Columns = ["Column0"], Rows = [["Value"]] }] }</example>
    public List<WorksheetData> Tables { get; init; } = [];
}

/// <summary>
/// Parsed worksheet data.
/// </summary>
public class WorksheetData
{
    /// <summary>
    /// Name of the worksheet.
    /// </summary>
    /// <example>Sheet1</example>
    public string TableName { get; init; } = string.Empty;

    /// <summary>
    /// Column names in the worksheet.
    /// </summary>
    /// <example>["Column0", "Column1"]</example>
    public List<string> Columns { get; init; } = [];

    /// <summary>
    /// Row values in the worksheet.
    /// </summary>
    /// <example>[["Order-1001", 42]]</example>
    public List<List<object?>> Rows { get; init; } = [];
}
