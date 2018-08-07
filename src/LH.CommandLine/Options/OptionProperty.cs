using System;
using System.Collections.Generic;
using System.Reflection;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options
{
    internal class OptionProperty
    {
        public static IReadOnlyList<OptionProperty> GetOptionProperties(Type type)
        {
            var optionProperties = new List<OptionProperty>();
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var propertyInfo in propertyInfos)
            {
                var optionAttribute = propertyInfo.GetCustomAttribute<OptionAttribute>();
                var argumentAttribute = propertyInfo.GetCustomAttribute<ArgumentAttribute>();

                if (optionAttribute != null)
                {
                    var optionProperty = new OptionProperty(optionAttribute, argumentAttribute, propertyInfo);

                    optionProperties.Add(optionProperty);
                }
            }

            return optionProperties;
        }

        private readonly IValueParser _valueParser;
        private readonly OptionAttribute _optionAttribute;
        private readonly ArgumentAttribute _argumentAttribute;

        private OptionProperty(OptionAttribute optionAttribute, ArgumentAttribute argumentAttribute, PropertyInfo propertyInfo)
        {
            _optionAttribute = optionAttribute;
            _argumentAttribute = argumentAttribute;
            _valueParser = ValueParsers.GetValueParser(propertyInfo.PropertyType);

            PropertyInfo = propertyInfo;
        }

        public bool IsPositional
        {
            get => _argumentAttribute != null;
        }

        public int PositionalIndex
        {
            get => _argumentAttribute.Index;
        }

        public PropertyInfo PropertyInfo { get; }

        public bool IsNamed
        {
            get => _optionAttribute != null;
        }

        public IReadOnlyList<string> Aliases
        {
            get => _optionAttribute.Aliases;
        }

        public object Value { get; private set; }

        public string Name
        {
            get => PropertyInfo.Name;
        }

        public Type Type
        {
            get => PropertyInfo.PropertyType;
        }

        public bool IsSwitch
        {
            get => false;
        }

        public void SetValue(string input)
        {
            if (Value != null)
            {
                throw new Exception("Value specified multiple times!");
            }

            var parsedValue = _valueParser.Parse(input);

            Value = parsedValue;
        }

        public void SetSwitchValue()
        {
            // TODO: Add switch attribute
            if (!IsSwitch)
            {
                throw new InvalidOperationException("Cannot set switch value on optiopn property which is not a switch");
            }

            Value = true;
        }
    }
}