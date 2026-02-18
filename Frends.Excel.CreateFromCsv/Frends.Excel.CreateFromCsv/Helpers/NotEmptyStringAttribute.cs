using System.ComponentModel.DataAnnotations;

namespace Frends.Excel.CreateFromCsv.Helpers;

internal class NotEmptyStringAttribute : RequiredAttribute
{
    public NotEmptyStringAttribute()
    {
        AllowEmptyStrings = false;
        ErrorMessage = "{0} is required and cannot be empty.";
    }
}
