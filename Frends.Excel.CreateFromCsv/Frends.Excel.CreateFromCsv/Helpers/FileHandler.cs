using System;
using System.Globalization;
using System.IO;

namespace Frends.Excel.CreateFromCsv.Helpers;

internal static class FileHandler
{
    internal static string Rename(string path)
    {
        var result = path;
        var parent = Path.GetDirectoryName(path);
        var orgName = Path.GetFileNameWithoutExtension(path);
        int counter = 1;

        while (File.Exists(result))
        {
            var newName = $"{orgName} ({counter}){Path.GetExtension(path)}";
            result = Path.Combine(parent ?? string.Empty, newName);
            counter++;
        }

        return result;
    }

    internal static object ParseValue(string value, CultureInfo culture)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        if (double.TryParse(value, NumberStyles.Any, culture, out double number)) return number;
        if (DateTime.TryParse(value, culture, DateTimeStyles.None, out DateTime date)) return date;

        return value;
    }
}
