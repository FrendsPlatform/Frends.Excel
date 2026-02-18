using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Frends.Excel.CreateFromCsv.Helpers;

internal class ValidExtensionAttribute : ValidationAttribute
{
    private readonly string[] extensions;
    private readonly bool allowMissingExtension;

    public ValidExtensionAttribute(string[] extensions, bool allowMissingExtension = false)
    {
        this.extensions = extensions
            .Select(x => x.StartsWith('.') ? x.ToLower() : x == string.Empty ? x : '.' + x.ToLower())
            .ToArray();
        this.allowMissingExtension = allowMissingExtension;

        ErrorMessage = "{0} has an invalid extension. Allowed extensions are: " + string.Join(", ",
            $"'{this.extensions}'" + (allowMissingExtension ? ". Missing extension is allowed." : '.'));
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        switch (value)
        {
            case null:
                return ValidationResult.Success;
            case string path:
                try
                {
                    string extension = Path.GetExtension(path);

                    if ((string.IsNullOrEmpty(extension) && allowMissingExtension) ||
                        extensions.Contains(extension.ToLower()))
                    {
                        return ValidationResult.Success;
                    }
                }
                catch (ArgumentException)
                {
                    return new ValidationResult($"{validationContext.DisplayName} has an invalid path format.");
                }

                break;
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
}
