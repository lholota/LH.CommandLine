using System;
using LH.CommandLine.Extensions;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options2.Validation
{
    internal class OptionPropertyMetadataValidator
    {
        private static readonly Type CustomParserBaseType = typeof(ValueParserBase<>);

        private readonly OptionPropertyMetadata _propertyMetadata;

        public OptionPropertyMetadataValidator(OptionPropertyMetadata propertyMetadata)
        {
            _propertyMetadata = propertyMetadata;
        }

        public ValidationResult ValidatePropertyMetadata()
        {
            var result = new ValidationResult();

            if (_propertyMetadata.HasDefaultValue && !IsValidValue(_propertyMetadata.DefaultValue))
            {
                result.AddError(GetInvalidValueTypeErrorMessage(_propertyMetadata.DefaultValue));
            }

            foreach (var switchValue in _propertyMetadata.Switches.Values)
            {
                if (!IsValidValue(switchValue))
                {
                    result.AddError(GetInvalidValueTypeErrorMessage(switchValue));
                }
            }

            if (_propertyMetadata.HasCustomParser &&
                !_propertyMetadata.CustomParserType.IsSubclassOfGeneric(CustomParserBaseType))
            {
                result.AddError($"The value parser type {_propertyMetadata.CustomParserType} is not derived from {CustomParserBaseType}");
            }

            return result;
        }

        private bool IsValidValue(object value)
        {
            if (value != null && _propertyMetadata.PropertyType.IsInstanceOfType(value))
            {
                return false;
            }

            if (value == null && !_propertyMetadata.PropertyType.IsByRef)
            {
                return false;
            }

            return true;
        }

        private string GetInvalidValueTypeErrorMessage(object value)
        {
            var valueString = value == null ? "null" : $"of type {value.GetType()}";
            return $"The switch value {valueString} cannot be assigned to property of type {_propertyMetadata.PropertyType} (property name {_propertyMetadata.PropertyName})";
        }
    }
}