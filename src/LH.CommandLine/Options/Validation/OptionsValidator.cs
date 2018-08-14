using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LH.CommandLine.Options.Validation
{
    internal class OptionsValidator
    {
        public IReadOnlyCollection<ValidationResult> ValidateOptions<TOptions>(TOptions options)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(options, null, null);

            if (!Validator.TryValidateObject(options, validationContext, validationResults, true))
            {
                return validationResults;
            }

            return new ValidationResult[0];
        }
    }
}