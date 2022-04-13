namespace Frends.Excel.ConvertToXML.Definitions;

/// <summary>
/// Available date formats.
/// </summary>
public enum DateFormats
{
    /// <summary>
    /// Default value specified by system settings. 
    /// Uses either `CultureInfo.CurrentCulture.DateTimeFormat` or 
    /// `CurrentCulture.DateTimeFormat.ShortDatePattern` depending 
    /// on `ShortDatePattern` flag.
    /// </summary>
    DEFAULT,
    /// <summary>
    /// Day/Month/Year (with leading zeroes).
    /// </summary>
    DDMMYYYY,
    /// <summary>
    /// Month/Day/Year (with leading zeroes).
    /// </summary>
    MMDDYYYY,
    /// <summary>
    /// Year/Month/Day (with leading zeroes).
    /// </summary>
    YYYYMMDD
}
