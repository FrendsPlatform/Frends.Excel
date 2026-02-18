using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Frends.Excel.CreateFromCsv.Helpers;

internal class ExistingFilePathAttribute : ValidationAttribute
{
    public ExistingFilePathAttribute()
    {
        ErrorMessage = "{0} must be a valid, existing file path.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        switch (value)
        {
            case null:
            case string path when File.Exists(path):
                return ValidationResult.Success;
            default:
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
