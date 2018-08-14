using LH.CommandLine.Options.Descriptors;
using LH.CommandLine.Options.Reflection;

namespace LH.CommandLine.Options
{
    internal class OptionsValues
    {
        public OptionsValues(OptionsTypeDescriptor typeDescriptor)
        {
            // When not default, throw
            // When collection, keep default on the side, it's a completely separate instance
        }

        public void SetValue(OptionProperty property, object value)
        {

        }

        private class OptionValue
        {
            public OptionValue(bool hasDefault, object defaultValue)
            {
                
            }

            public object Value { get; }
        }
    }
}
//    internal class OptionsValues : IReadOnlyCollection<PropertyValue>
//    {
//        private readonly IDictionary<OptionProperty, IOptionValue> _values;

//        public OptionsValues(OptionsTypeDescriptor typeDescriptor)
//        {
//            _values = CreateInitialValues(typeDescriptor);
//        }

//        public int Count
//        {
//            get => _values.Count;
//        }

//        public void SetValue(PropertyValue propertyValue)
//        {
//            SetValue(propertyValue.PropertyInfo, propertyValue.Value);
//        }

//        public void SetValue(OptionProperty optionProperty, object value)
//        {
//            _values[optionProperty].SetValue(value);
//        }

//        public IEnumerator<PropertyValue> GetEnumerator()
//        {
//            return _values.Select(x => new PropertyValue(x.Key, x.Value.GetValue())).GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }

//        private IDictionary<OptionProperty, IOptionValue> CreateInitialValues(OptionsTypeDescriptor typeDescriptor)
//        {
//            var result = new Dictionary<OptionProperty, IOptionValue>();

//            foreach (var property in typeDescriptor.Properties)
//            {
//                if(property is CollectionOptionProperty collectionOptionProperty)
//                {
//                    result[property] = new CollectionOptionValue(collectionOptionProperty);
//                }
//                else
//                {
//                    result[property] = new OptionValue(property);
//                }
//            }

//            return result;
//        }

//        private interface IOptionValue
//        {
//            void SetValue(object value);

//            object GetValue();
//        }

//        private class OptionValue : IOptionValue
//        {
//            private object _value;
//            private bool _isDefault;

//            private readonly string _propertyName;

//            public OptionValue(object defaultValue, string propertyName)
//            {
//                _value = defaultValue;
//                _propertyName = propertyName;
//                _isDefault = true;
//            }

//            public void SetValue(object value)
//            {
//                if (!_isDefault)
//                {
//                    throw new DuplicateValueException(_propertyName);
//                }

//                _value = value;
//                _isDefault = false;
//            }

//            public object GetValue()
//            {
//                return _value;
//            }
//        }

//        private class CollectionOptionValue : IOptionValue
//        {
//            private readonly Type _targetType;
//            private readonly Type _collectionType;
//            private readonly Type _itemType;
//            private readonly List<object> _items;
//            private readonly object _defaultValue;

//            private bool _useDefault;

//            public CollectionOptionValue(Type collectionType, Type itemType, object defaultValue)
//            {
//                _defaultValue = defaultValue;
//                _useDefault = true;

//                _collectionType = collectionType;
//                _itemType = itemType;
//                _items = new List<object>();
//            }

//            public void SetValue(object item)
//            {
//                _useDefault = false;

//                _items.Add(item);
//            }

//            public object GetValue()
//            {
//                // Create an instance of the right collection type (List, Collection, Array)
//                // Add values into it using reflection

//                return null;
//            }
//        }
//    }
//}