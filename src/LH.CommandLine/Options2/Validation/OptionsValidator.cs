using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationError = System.ComponentModel.DataAnnotations.ValidationResult;

namespace LH.CommandLine.Options2.Validation
{
    internal class OptionsValidator
    {
        public ValidationResult ValidateOptions<TOptions>(TOptions options)
        {
            var errors = new List<ValidationError>();
            var validationContext = new ValidationContext(options, null, null);

            var result = new ValidationResult();

            if (!Validator.TryValidateObject(options, validationContext, errors, true))
            {
                foreach (var error in errors)
                {
                    result.AddError(error.ErrorMessage);
                }
            }

            return result;
        }
    }
}