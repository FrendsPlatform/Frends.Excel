using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Frends.Excel.CreateFromCsv.Helpers;

internal class ValidExtensionAttribute : ValidationAttribute
{
    private readonly string[] extensions;

    public ValidExtensionAttribute(params string[] extensions)
    {
        this.extensions = extensions
            .Select(x => x.StartsWith('.') ? x.ToLower() : x == string.Empty ? x : '.' + x.ToLower())
            .ToArray();

        ErrorMessage = "{0} has an invalid extension. Allowed extensions are: " + string.Join(", ", this.extensions);
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        switch (value)
        {
            case string path:
                try
                {
                    string extension = Path.GetExtension(path);

                    if (extensions.Contains(extension.ToLower()))
                        return ValidationResult.Success;
                }
                catch (ArgumentException)
                {
                    return new ValidationResult("Invalid path format.");
                }

                break;
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
}
