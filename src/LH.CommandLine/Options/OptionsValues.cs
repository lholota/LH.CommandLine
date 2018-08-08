using System;
using System.Collections.Generic;
using System.Reflection;
using LH.CommandLine.Exceptions;

namespace LH.CommandLine.Options
{
    internal class OptionsValues
    {
        private readonly IDictionary<PropertyInfo, OptionValue> _values;

        public OptionsValues()
        {
            _values = new Dictionary<PropertyInfo, OptionValue>();
        }

        public void SetValue(OptionProperty property, object value)
        {
            VerifyCanSetValue(property);

            _values[property.PropertyInfo] = new OptionValue(value, false);
        }

        public void SetDefaultValue(OptionProperty property)
        {
            if (!property.HasDefaultValue)
            {
                throw new InvalidOperationException("Cannot set default value on an OptionProperty which doesn't have a default value.");
            }

            VerifyCanSetValue(property);

            _values[property.PropertyInfo] = new OptionValue(property.DefaultValue, true);
        }

        private void VerifyCanSetValue(OptionProperty property)
        {
            if (_values.TryGetValue(property.PropertyInfo, out var value))
            {
                if (!value.IsDefault)
                {
                    throw new DuplicateValueException(property.PropertyInfo.Name);
                }
            }
        }

        private class OptionValue
        {
            public OptionValue(object value, bool isDefault)
            {
                Value = value;
                IsDefault = isDefault;
            }

            public object Value { get; }

            public bool IsDefault { get; }
        }
    }
}