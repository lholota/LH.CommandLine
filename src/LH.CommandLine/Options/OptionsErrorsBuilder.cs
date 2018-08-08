using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LH.CommandLine.Exceptions;

namespace LH.CommandLine.Options
{
    internal class OptionsErrorsBuilder
    {
        private readonly List<string> _errors;

        public OptionsErrorsBuilder()
        {
            _errors = new List<string>();
        }

        public bool HasErrors
        {
            get => _errors.Any();
        }


        public void AddInvalidOptionError(string optionName)
        {
            _errors.Add($"Invalid option {optionName}");
        }

        public void AddInvalidValueError(OptionProperty property, string rawValue)
        {
            _errors.Add($"The value '{rawValue}' is not valid for the option of type {property.Type}.");
        }

        public void AddValidationErrors(IEnumerable<ValidationResult> validationResults)
        {
            var errorMessages = validationResults.Select(x => x.ErrorMessage);

            _errors.AddRange(errorMessages);
        }

        public InvalidOptionsException BuildException()
        {
            return new InvalidOptionsException(_errors);
        }
    }
}