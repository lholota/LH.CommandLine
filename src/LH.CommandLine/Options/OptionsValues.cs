using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options
{

    internal class OptionsValues : IReadOnlyCollection<PropertyValue>
    {
        private readonly IDictionary<OptionPropertyMetadata, OptionValue> _values;

        public OptionsValues(OptionsMetadata optionsMetadata)
        {
            _values = CreateInitialValues(optionsMetadata);
        }

        public int Count
        {
            get => _values.Count;
        }

        public void SetValue(PropertyValue propertyValue)
        {
            SetValue(propertyValue.PropertyMetadata, propertyValue.Value);
        }

        public void SetValue(OptionPropertyMetadata propertyMetadata, object value)
        {
            _values[propertyMetadata].SetValue(value);
        }

        public IEnumerator<PropertyValue> GetEnumerator()
        {
            return _values.Select(x => new PropertyValue(x.Key, x.Value.GetValue())).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IDictionary<OptionPropertyMetadata, OptionValue> CreateInitialValues(OptionsMetadata optionsMetadata)
        {
            var result = new Dictionary<OptionPropertyMetadata, OptionValue>();

            foreach (var property in optionsMetadata.Properties)
            {
                if (property.HasDefaultValue)
                {
                    result[property] = new OptionValue(property.DefaultValue, property.Name);
                }
                else
                {
                    result[property] = new OptionValue(null, property.Name);
                }
            }

            return result;
        }

        private class OptionValue
        {
            private object _value;
            private bool _isDefault;

            private readonly string _propertyName;

            public OptionValue(object defaultValue, string propertyName)
            {
                _value = defaultValue;
                _propertyName = propertyName;
                _isDefault = true;
            }

            public void SetValue(object value)
            {
                if (!_isDefault)
                {
                    throw new DuplicateValueException(_propertyName);
                }

                _value = value;
                _isDefault = false;
            }

            public object GetValue()
            {
                return _value;
            }
        }
    }
}