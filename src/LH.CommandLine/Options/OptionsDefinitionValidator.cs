using System.Collections.Generic;
using System.Linq;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Extensions;
using LH.CommandLine.Options.Descriptors;
using LH.CommandLine.Options.Factoring;
using LH.CommandLine.Options.Reflection;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options
{
    internal class OptionsDefinitionValidator<TOptions>
    {
        private readonly OptionsTypeDescriptor _typeDescriptor;
        private readonly IOptionsFactory<TOptions> _optionsFactory;
        private readonly ValueParserSelector _valueParserSelector;

        public OptionsDefinitionValidator(
            OptionsTypeDescriptor typeDescriptor,
            IOptionsFactory<TOptions> optionsFactory,
            ValueParserSelector valueParserSelector)
        {
            _typeDescriptor = typeDescriptor;
            _optionsFactory = optionsFactory;
            _valueParserSelector = valueParserSelector;
        }

        public void Validate()
        {
            var errors = new List<string>();

            foreach (var property in _typeDescriptor.Properties)
            {
                ValidateDefaultValue(errors, property);
                ValidateSwitchValues(errors, property);
                ValidateCustomValueParser(errors, property);
                ValidateCanBeParsed(errors, property);
            }

            ValidateOptionsCanBeConstruted(errors);
            ValidatePositionalIndexes(errors);
            ValidateAliases(errors);

            if (errors.Count > 0)
            {
                throw new InvalidOptionsDefinitionException(_typeDescriptor.OptionsType, errors);
            }
        }

        private void ValidateCanBeParsed(ICollection<string> errors, OptionProperty property)
        {
            if (!_valueParserSelector.HasParserForProperty(property))
            {
                errors.Add($"There is no parser for the type {property.Type}, please create your own implementation and use the ValueParserAttribute on the property {property.Name}.");
            }
        }

        private void ValidateDefaultValue(ICollection<string> errors, OptionProperty property)
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

        private void ValidateSwitchValues(ICollection<string> errors, OptionProperty property)
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

        private void ValidateCustomValueParser(ICollection<string> errors, OptionProperty property)
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
            var indexes = _typeDescriptor.Properties
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
            var optionAliases = _typeDescriptor.Properties.SelectMany(x => x.OptionAliases);
            var switchAliases = _typeDescriptor.Properties.SelectMany(x => x.Switches).Select(x => x.Key);

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
    }
}