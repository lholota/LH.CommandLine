using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LH.CommandLine.Options
{
    internal class OptionsValidator
    {
        public void ValidateOptions<TOptions>(OptionsErrorsBuilder errorsBuilder, TOptions options)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(options, null, null);

            if (!Validator.TryValidateObject(options, validationContext, validationResults, true))
            {
                errorsBuilder.AddValidationErrors(validationResults);
            }
        }
    }
}