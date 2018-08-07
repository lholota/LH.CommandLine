using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LH.CommandLine.Options
{
    internal class OptionsValidator
    {
        public void ValidateOptions<TOptions>(TOptions options)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this, null, null);

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                // var message = String.Join(Environment.NewLine, validationResults.Select(vr => vr.ErrorMessage));

                throw new Exception("Add exception");
            }
        }
    }
}