namespace Frends.Excel.Parse.Definitions;

/// <summary>
/// Specifies the supported date component orderings.
/// </summary>
/// <remarks>Includes default, day-month-year, month-day-year, and year-month-day options.</remarks>
public enum DateFormats
{
    /// <summary>
    /// default value
    /// </summary>
    DEFAULT,

    /// <summary>
    /// day/month/year
    /// </summary>
    DDMMYYYY,

    /// <summary>
    /// month/day/year
    /// </summary>
    MMDDYYYY,

    /// <summary>
    /// year/month/day
    /// </summary>
    YYYYMMDD,
}
