using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options
{
    internal class OptionsParsingContext
    {
        private readonly List<string> _errors;
        private readonly IDictionary<OptionPropertyMetadata, IOptionValue> _values;

        public OptionsParsingContext(OptionsMetadata optionsMetadata)
        {
            _values = CreateInitialValues(optionsMetadata);
            _errors = new List<string>();
        }

        public IReadOnlyList<string> Errors
        {
            get => _errors;
        }

        public bool HasErrors
        {
            get => Errors.Any();
        }

        public void AddValue(OptionPropertyMetadata propertyMetadata, object value)
        {
            _values[propertyMetadata].AddValue(value);
        }

        public void AddInvalidOptionError(string optionName)
        {
            _errors.Add($"Invalid option {optionName}");
        }

        public void AddInvalidValueError(OptionPropertyMetadata propertyMetadata, string rawValue)
        {
            _errors.Add($"The value '{rawValue}' is not valid for the option of type {propertyMetadata.ParsedType}.");
        }

        public void AddSpecifiedMultipleTimesError(OptionPropertyMetadata propertyMetadata)
        {
            _errors.Add($"The value for the property {propertyMetadata.Name} has been specified multiple times.");
        }

        public void AddValidationErrors(IEnumerable<ValidationResult> validationResults)
        {
            var errorMessages = validationResults.Select(x => x.ErrorMessage);

            _errors.AddRange(errorMessages);
        }

        public IReadOnlyCollection<PropertyValue> GetValues()
        {
            return _values
                .Select(x => new PropertyValue(x.Key, x.Value.GetValue()))
                .ToArray();
        }

        private IDictionary<OptionPropertyMetadata, IOptionValue> CreateInitialValues(OptionsMetadata optionsMetadata)
        {
            var result = new Dictionary<OptionPropertyMetadata, IOptionValue>();

            foreach (var property in optionsMetadata.Properties)
            {
                IOptionValue value;

                if (property.IsCollection)
                {
                    value = new OptionCollectionValue(property);
                }
                else
                {
                    value = new OptionValue(property);
                }

                result.Add(property, value);
            }

            return result;
        }
    }
}