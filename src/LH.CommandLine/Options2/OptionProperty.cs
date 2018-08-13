using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using LH.CommandLine.Options;

namespace LH.CommandLine.Options2
{
    internal class OptionProperty
    {
        private readonly Type _customParserType;
        private readonly string[] _optionAliases;
        private readonly object _defaultValue;
        private readonly int? _positionalIndex;
        private readonly IDictionary<string, object> _switches;

        public OptionProperty(PropertyInfo propertyInfo)
        {
            _optionAliases = GetOptionAliases(propertyInfo);
            _switches = GetSwitches(propertyInfo);
            _defaultValue = GetDefaultValue(propertyInfo);
            _positionalIndex = GetPositionalIndex(propertyInfo);
            _customParserType = GetCustomParserType(propertyInfo);
        }

        public bool IsValid(out IEnumerable<string> errors)
        {

        }

        private string[] GetOptionAliases(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes<OptionAttribute>()
                .SelectMany(x => x.Aliases)
                .ToArray();
        }

        private IDictionary<string, object> GetSwitches(PropertyInfo propertyInfo)
        {
            var switches = new Dictionary<string, object>();
            var switchAttributes = propertyInfo.GetCustomAttributes<SwitchAttribute>();

            foreach (var switchAttribute in switchAttributes)
            {
                foreach (var alias in switchAttribute.Aliases)
                {
                    switches.Add(alias, switchAttribute.Value);
                }
            }

            return switches;
        }

        private object GetDefaultValue(PropertyInfo propertyInfo)
        {
            var defaultValueAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();

            if (defaultValueAttribute != null)
            {
                return defaultValueAttribute.Value;
            }

            if (propertyInfo.PropertyType.IsValueType)
            {
                return Activator.CreateInstance(propertyInfo.PropertyType); // Equivalent of default(T) for value types
            }

            return null;
        }

        private int? GetPositionalIndex(PropertyInfo propertyInfo)
        {
            var argumentAttribute = propertyInfo.GetCustomAttribute<ArgumentAttribute>();

            return argumentAttribute?.Index;
        }

        private Type GetCustomParserType(PropertyInfo propertyInfo)
        {
            var valueParserAttribute = propertyInfo.GetCustomAttribute<ValueParserAttribute>();

            return valueParserAttribute?.ParserType;
        }
    }
}