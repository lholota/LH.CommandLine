using System.Collections.Generic;
using System.Linq;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Extensions;
using LH.CommandLine.Options.Factoring;
using LH.CommandLine.Options.Metadata;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.Validation
{
    internal class OptionsMetadataValidator<TOptions>
    {
        private readonly OptionsMetadata _optionsMetadata;
        private readonly IOptionsFactory<TOptions> _optionsFactory;
        private readonly ValueParserSelector _valueParserSelector;

        public OptionsMetadataValidator(
            OptionsMetadata optionsMetadata,
            IOptionsFactory<TOptions> optionsFactory,
            ValueParserSelector valueParserSelector)
        {
            _optionsMetadata = optionsMetadata;
            _optionsFactory = optionsFactory;
            _valueParserSelector = valueParserSelector;
        }

        public void Validate()
        {
            var errors = new List<string>();

            foreach (var property in _optionsMetadata.Properties)
            {
                ValidateDefaultValue(errors, property);
                ValidateSwitchValues(errors, property);
                ValidateCustomValueParser(errors, property);
                ValidateCanBeParsed(errors, property);
            }

            ValidateOnlyLastPositionalCanBeCollection(errors);
            ValidateOptionsCanBeConstruted(errors);
            ValidatePositionalIndexes(errors);
            ValidateAliases(errors);

            if (errors.Count > 0)
            {
                throw new InvalidOptionsDefinitionException(_optionsMetadata.OptionsType, errors);
            }
        }

        private void ValidateCanBeParsed(ICollection<string> errors, OptionPropertyMetadata property)
        {
            if (!_valueParserSelector.HasParserForProperty(property))
            {
                errors.Add($"There is no parser for the type {property.Type}, please create your own implementation and use the ValueParserAttribute on the property {property.Name}.");
            }
        }

        private void ValidateDefaultValue(ICollection<string> errors, OptionPropertyMetadata property)
        {
            if (property.HasDefaultValue)
            {
                var defaultValueType = property.DefaultValue?.GetType();

                if (!property.Type.IsAssignableFrom(defaultValueType))
                {
                    errors.Add(
                        $"The default value of type {defaultValueType} cannot be assigned to property of type {property.Type} (property name {property.Name})");
                }
            }
        }

        private void ValidateSwitchValues(ICollection<string> errors, OptionPropertyMetadata property)
        {
            foreach (var switchValue in property.Switches.Values)
            {
                if (!property.Type.IsInstanceOfType(switchValue.Value))
                {
                    errors.Add(
                        $"The switch value of type {switchValue.Value.GetType()} cannot be assigned to property of type {property.Type} (property name {property.Name})");
                }
            }
        }

        private void ValidateCustomValueParser(ICollection<string> errors, OptionPropertyMetadata property)
        {
            if (!property.HasCustomParser)
            {
                return;
            }

            var baseType = typeof(ValueParserBase<>);

            if (!property.CustomParserType.IsSubclassOfGeneric(baseType))
            {
                errors.Add($"The value parser type {property.CustomParserType} is not derived from {baseType}");
            }
        }

        private void ValidatePositionalIndexes(ICollection<string> errors)
        {
            var indexes = _optionsMetadata.Properties
                .Where(x => x.HasPositionalIndex)
                .Select(x => x.PositionalIndex)
                .OrderBy(x => x)
                .ToArray();

            for (var i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] != i)
                {
                    if (i == 0)
                    {
                        errors.Add("The argument indexes must start with 0.");
                    }
                    else
                    {
                        errors.Add($"The argument indexes must be continous. There is no argument with index {i}.");
                    }
                }
            }
        }

        private void ValidateAliases(ICollection<string> errors)
        {
            var optionAliases = _optionsMetadata.Properties.SelectMany(x => x.OptionAliases);
            var switchAliases = _optionsMetadata.Properties.SelectMany(x => x.Switches).Select(x => x.Key);

            var groupedAliases = optionAliases
                .Concat(switchAliases)
                .GroupBy(x => x);

            foreach (var group in groupedAliases)
            {
                if (group.Count() > 1)
                {
                    errors.Add($"The alias '{group.Key}' is used for more than one Option or Switch.");
                }
            }
        }

        private void ValidateOptionsCanBeConstruted(ICollection<string> errors)
        {
            if (!_optionsFactory.CanCreateOptions())
            {
                errors.Add($"Cannot create an instance of type {typeof(TOptions)}. The options must be either a type with a parameterless constructor and public setters or a type with constructor parameters for all properties.");
            }
        }

        private void ValidateOnlyLastPositionalCanBeCollection(ICollection<string> errors)
        {
            var maxIndex = -1;
            var collectionIndex = -1;

            foreach (var propertyMetadata in _optionsMetadata.Properties)
            {
                if (!propertyMetadata.HasPositionalIndex)
                {
                    continue;
                }

                if (maxIndex < propertyMetadata.PositionalIndex)
                {
                    maxIndex = propertyMetadata.PositionalIndex;
                }

                if (propertyMetadata.IsCollection)
                {
                    if (collectionIndex != -1)
                    {
                        errors.Add("Only one Argument can be a collection.");
                    }

                    collectionIndex = propertyMetadata.PositionalIndex;
                }
            }

            if (collectionIndex != -1 && collectionIndex < maxIndex)
            {
                errors.Add("Only the Argument with the highest index can be a collection.");
            }
        }
    }
}