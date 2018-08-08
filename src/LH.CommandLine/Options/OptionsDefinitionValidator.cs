using System;
using System.Collections.Generic;
using System.Linq;
using LH.CommandLine.Exceptions;

namespace LH.CommandLine.Options
{
    internal class OptionsDefinitionValidator
    {
        private readonly Type _optionsType;
        private readonly OptionPropertyCollection _optionProperties;

        public OptionsDefinitionValidator(Type optionsType, OptionPropertyCollection optionProperties)
        {
            _optionsType = optionsType;
            _optionProperties = optionProperties;
        }

        public void Validate()
        {
            var errors = new List<string>();

            CheckNamesAreUnique(errors);
            CheckDefaultValueTypes(errors);
            CheckSwitchValueTypes(errors);

            if (errors.Count > 0)
            {
                throw new InvalidOptionsDefinitionException(_optionsType, errors);
            }
        }

        private void CheckNamesAreUnique(IList<string> errors)
        {
            var optionAliases = _optionProperties
                .Where(x => x.IsOption)
                .SelectMany(x => x.OptionAliases);

            var switchAliases = _optionProperties
                .Where(x => x.HasSwitches)
                .SelectMany(x => x.Switches)
                .SelectMany(x => x.Aliases);

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

        private void CheckDefaultValueTypes(IList<string> errors)
        {
            foreach (var optionProperty in _optionProperties)
            {
                if (optionProperty.HasDefaultValue)
                {
                    var defaultValueType = optionProperty.DefaultValue?.GetType();

                    if (defaultValueType != null && !optionProperty.Type.IsAssignableFrom(defaultValueType))
                    {
                        errors.Add($"The default value of type {defaultValueType} cannot be assigned to property of type {optionProperty.Type} (property name {optionProperty.PropertyInfo.Name})");
                    }
                }
            }
        }

        private void CheckSwitchValueTypes(IList<string> errors)
        {
            foreach (var optionProperty in _optionProperties)
            {
                foreach (var @switch in optionProperty.Switches)
                {
                    var switchValueType = @switch.Value?.GetType();

                    if (switchValueType != null && optionProperty.Type != switchValueType)
                    {
                        errors.Add($"The switch value of type {switchValueType} cannot be assigned to property of type {optionProperty.Type} (property name {optionProperty.PropertyInfo.Name})");
                    }
                }
            }
        }
    }
}