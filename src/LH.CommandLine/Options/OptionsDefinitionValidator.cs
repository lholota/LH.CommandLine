using System.Collections.Generic;
using System.Linq;
using LH.CommandLine.Exceptions;

namespace LH.CommandLine.Options
{
    internal class OptionsDefinitionValidator
    {
        private readonly OptionsTypeDescriptor _typeDescriptor;

        public OptionsDefinitionValidator(OptionsTypeDescriptor typeDescriptor)
        {
            _typeDescriptor = typeDescriptor;
        }

        public void Validate()
        {
            var errors = new List<string>();

            CheckNamesAreUnique(errors);
            CheckDefaultValueTypes(errors);
            CheckSwitchValueTypes(errors);

            if (errors.Count > 0)
            {
                throw new InvalidOptionsDefinitionException(_typeDescriptor.OptionsType, errors);
            }
        }

        private void CheckNamesAreUnique(IList<string> errors)
        {
            var groupedAliases = _typeDescriptor
                .GetAliases()
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
            foreach (var defaultValue in _typeDescriptor.DefaultValues)
            {
                if (!defaultValue.IsValid())
                {
                    errors.Add($"The default value of type {defaultValue.ValueType} cannot be assigned to property of type {defaultValue.PropertyType} (property name {defaultValue.PropertyName})");
                }
            }
        }

        private void CheckSwitchValueTypes(IList<string> errors)
        {
            var switchValues = _typeDescriptor.GetSwitchValues();

            foreach (var switchValue in switchValues)
            {
                if (!switchValue.IsValid())
                {
                    errors.Add($"The switch value of type {switchValue.ValueType} cannot be assigned to property of type {switchValue.PropertyType} (property name {switchValue.PropertyName})");
                }
            }
        }
    }
}