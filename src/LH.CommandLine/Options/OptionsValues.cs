using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LH.CommandLine.Exceptions;

namespace LH.CommandLine.Options
{
    internal class OptionsValues : IReadOnlyCollection<PropertyValue>
    {
        private readonly IDictionary<PropertyInfo, OptionValue> _values;

        public OptionsValues(OptionsTypeDescriptor typeDescriptor)
        {
            _values = CreateInitialValues(typeDescriptor);
        }

        public int Count
        {
            get => _values.Count;
        }

        public void SetValue(PropertyValue propertyValue)
        {
            SetValue(propertyValue.PropertyInfo, propertyValue.Value);
        }

        public void SetValue(PropertyInfo propertyInfo, object value)
        {
            if (IsValueAlreadySet(propertyInfo))
            {
                throw new DuplicateValueException(propertyInfo.Name);
            }
            
            _values[propertyInfo] = new OptionValue(value, false);
        }

        public IEnumerator<PropertyValue> GetEnumerator()
        {
            return _values.Select(x => new PropertyValue(x.Key, x.Value?.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IDictionary<PropertyInfo, OptionValue> CreateInitialValues(OptionsTypeDescriptor typeDescriptor)
        {
            var values = typeDescriptor.Properties
                .ToDictionary(x => x, x => (OptionValue)null);

            foreach (var defaultValue in typeDescriptor.DefaultValues)
            {
                values[defaultValue.PropertyInfo] = new OptionValue(defaultValue.Value, true);
            }

            return values;
        }

        private bool IsValueAlreadySet(PropertyInfo propertyInfo)
        {
            return _values.TryGetValue(propertyInfo, out var existingValue)
                && existingValue != null
                && !existingValue.IsDefault;
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