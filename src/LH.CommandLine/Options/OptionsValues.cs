using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Extensions;
using LH.CommandLine.Options.Reflection;

namespace LH.CommandLine.Options
{
    internal class OptionsValues : IReadOnlyCollection<PropertyValue>
    {
        private readonly IDictionary<PropertyInfo, IOptionValue> _values;

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
            _values[propertyInfo].SetValue(value);
        }

        public IEnumerator<PropertyValue> GetEnumerator()
        {
            return _values.Select(x => new PropertyValue(x.Key, x.Value.GetValue())).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IDictionary<PropertyInfo, IOptionValue> CreateInitialValues(OptionsTypeDescriptor typeDescriptor)
        {
            var result = new Dictionary<PropertyInfo, IOptionValue>();

            foreach (var propertyInfo in typeDescriptor.Properties)
            {
                var defaultValue = typeDescriptor.DefaultValues.SingleOrDefault(x => x.PropertyInfo == propertyInfo);

                if (propertyInfo.PropertyType.IsCollection(out var itemType))
                {
                    result[propertyInfo] = new CollectionOptionValue(propertyInfo.PropertyType, itemType, defaultValue);
                }
                else
                {
                    result[propertyInfo] = new OptionValue(defaultValue, propertyInfo.Name);
                }
            }

            return result;
        }

        private interface IOptionValue
        {
            void SetValue(object value);

            object GetValue();
        }

        private class OptionValue : IOptionValue
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

        private class CollectionOptionValue : IOptionValue
        {
            private readonly Type _targetType;
            private readonly Type _collectionType;
            private readonly Type _itemType;
            private readonly List<object> _items;
            private readonly object _defaultValue;

            private bool _useDefault;

            public CollectionOptionValue(Type collectionType, Type itemType, object defaultValue)
            {
                _defaultValue = defaultValue;
                _useDefault = true;

                _collectionType = collectionType;
                _itemType = itemType;
                _items = new List<object>();
            }

            public void SetValue(object item)
            {
                _useDefault = false;

                _items.Add(item);
            }

            public object GetValue()
            {
                // Create an instance of the right collection type (List, Collection, Array)
                // Add values into it using reflection

                return null;
            }
        }
    }
}