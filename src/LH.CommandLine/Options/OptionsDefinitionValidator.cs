using System.Collections.Generic;
using System.Linq;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options
{
    internal class OptionsDefinitionValidator
    {
        private readonly OptionsTypeDescriptor _typeDescriptor;

        public OptionsDefinitionValidator(
            OptionsTypeDescriptor typeDescriptor)
        {
            _typeDescriptor = typeDescriptor;
        }

        public void Validate()
        {
            var errors = new List<string>();

            errors.AddRange(ValidateNamesAreUnique());
            errors.AddRange(ValidateDefaultValueTypes());
            errors.AddRange(ValidateSwitchValueTypes());
            errors.AddRange(CheckPositionalIndexes());
            errors.AddRange(ValidateValueParsers());

            if (errors.Count > 0)
            {
                throw new InvalidOptionsDefinitionException(_typeDescriptor.OptionsType, errors);
            }
        }

        private IEnumerable<string> ValidateNamesAreUnique()
        {
            var groupedAliases = _typeDescriptor
                .GetAliases()
                .GroupBy(x => x);

            foreach (var group in groupedAliases)
            {
                if (group.Count() > 1)
                {
                    yield return $"The alias '{group.Key}' is used for more than one Option or Switch.";
                }
            }
        }

        private IEnumerable<string> ValidateDefaultValueTypes()
        {
            foreach (var defaultValue in _typeDescriptor.DefaultValues)
            {
                if (!defaultValue.IsValid())
                {
                    yield return $"The default value of type {defaultValue.ValueType} cannot be assigned to property of type {defaultValue.PropertyType} (property name {defaultValue.PropertyName})";
                }
            }
        }

        private IEnumerable<string> ValidateSwitchValueTypes()
        {
            var switchValues = _typeDescriptor.GetSwitchValues();

            foreach (var switchValue in switchValues)
            {
                if (!switchValue.IsValid())
                {
                    yield return $"The switch value of type {switchValue.ValueType} cannot be assigned to property of type {switchValue.PropertyType} (property name {switchValue.PropertyName})";
                }
            }
        }

        private IEnumerable<string> CheckPositionalIndexes()
        {
            var indexes = _typeDescriptor.GetPositionalIndexes()
                .OrderBy(x => x)
                .ToArray();

            if (indexes.Length == 0)
            {
                yield break;
            }

            for (var i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] != i)
                {
                    if (i == 0)
                    {
                        yield return "The argument indexes must start with 0.";
                    }
                    else
                    {
                        yield return $"The argument indexes must be continous. There is no argument with index {i}.";
                    }
                }
            }
        }

        private IEnumerable<string> ValidateValueParsers()
        {
            var valueParserInterface = typeof(IValueParser);

            foreach (var parserType in _typeDescriptor.GetValueParserOverrideTypes())
            {
                if (!parserType.IsSubclassOf(typeof(ValueParserBase<>)))
                {
                    // TODO: Error
                }

                // TODO: Find the parser generic parameter and compare it with the property type

                if (!valueParserInterface.IsAssignableFrom(parserType))
                {
                    yield return $"The type {parserType} does not implement the IValueParser interface.";
                }
            }
        }
    }
}