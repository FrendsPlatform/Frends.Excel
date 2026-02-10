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
}
