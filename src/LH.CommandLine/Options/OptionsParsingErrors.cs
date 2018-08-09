using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace LH.CommandLine.Options
{
    internal class OptionsParsingErrors : IReadOnlyList<string>
    {
        private readonly List<string> _errors;

        public OptionsParsingErrors()
        {
            _errors = new List<string>();
        }

        public int Count
        {
            get => _errors.Count;
        }

        public string this[int index]
        {
            get => _errors[index];
        }

        public void AddInvalidOptionError(string optionName)
        {
            _errors.Add($"Invalid option {optionName}");
        }

        public void AddInvalidValueError(PropertyInfo property, string rawValue)
        {
            _errors.Add($"The value '{rawValue}' is not valid for the option of type {property.PropertyType}.");
        }

        public void AddSpecifiedMultipleTimesError(PropertyInfo propertyInfo)
        {
            _errors.Add($"The value for the property {propertyInfo.Name} has been specified multiple times.");
        }

        public void AddValidationErrors(IEnumerable<ValidationResult> validationResults)
        {
            var errorMessages = validationResults.Select(x => x.ErrorMessage);

            _errors.AddRange(errorMessages);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}