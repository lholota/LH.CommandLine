using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options.Metadata;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options
{
    internal class OptionsParsingContext
    {
        private readonly List<string> _errors;
        private readonly CollectionValueFactory _collectionValueFactory;
        private readonly IDictionary<OptionPropertyMetadata, IOptionValue> _values;

        public OptionsParsingContext(OptionsMetadata optionsMetadata)
        {
            _collectionValueFactory = new CollectionValueFactory();
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

        public void SetValue(OptionPropertyMetadata propertyMetadata, object value)
        {
            try
            {
                _values[propertyMetadata].AddValue(value);
            }
            catch (DuplicateValueException)
            {
                AddSpecifiedMultipleTimesError(propertyMetadata);
            }
        }

        public void SetValue(OptionPropertyMetadata propertyMetadata, string rawValue, IValueParser valueParser)
        {
            object parsedValue;

            try
            {
                parsedValue = valueParser.Parse(rawValue, propertyMetadata.ParsedType);
            }
            catch (Exception)
            {
                AddInvalidValueError(propertyMetadata, rawValue);
                return;
            }

            SetValue(propertyMetadata, parsedValue);
        }

        public void SetCollectionValue(OptionPropertyMetadata propertyMetadata, string[] rawValues, IValueParser valueParser)
        {
            var parsedValues = new List<object>();

            foreach (var rawValue in rawValues)
            {
                try
                {
                    var parsedValue = valueParser.Parse(rawValue, propertyMetadata.ParsedType);

                    parsedValues.Add(parsedValue);
                }
                catch (Exception)
                {
                    AddInvalidValueError(propertyMetadata, rawValue);
                }
            }

            var collection = _collectionValueFactory.CreateCollection(parsedValues, propertyMetadata);

            SetValue(propertyMetadata, collection);
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

        public void AddOptionWithoutValueError(string optionName)
        {
            _errors.Add($"The option '{optionName}' was specified without a value.");
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
                var value = new OptionValue(property);

                result.Add(property, value);
            }

            return result;
        }
    }
}