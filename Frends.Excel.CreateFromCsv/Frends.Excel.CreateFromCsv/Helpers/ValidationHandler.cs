using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Frends.Excel.CreateFromCsv.Definitions;

namespace Frends.Excel.CreateFromCsv.Helpers;

internal static class ValidationHandler
{
    internal static string Validate(Input input)
    {
        var inputContext = new ValidationContext(input);
        List<ValidationResult> validateResults = [];
        Validator.TryValidateObject(input, inputContext, validateResults, true);

        return validateResults.Aggregate(string.Empty, (current, error) => current + $"{error.ErrorMessage}\n");
    }
}
