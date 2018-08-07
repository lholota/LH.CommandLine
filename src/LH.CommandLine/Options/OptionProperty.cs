using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace LH.CommandLine.Options
{
    internal class OptionProperty
    {
        private readonly OptionAttribute _optionAttribute;
        private readonly ArgumentAttribute _argumentAttribute;
        private readonly IReadOnlyList<SwitchAttribute> _switchAttributes;

        public static bool IsOptionProperty(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<OptionAttribute>() != null
                   || propertyInfo.GetCustomAttribute<ArgumentAttribute>() != null
                   || propertyInfo.GetCustomAttributes<SwitchAttribute>().Any();
        }

        internal OptionProperty(PropertyInfo propertyInfo)
        {
            _switchAttributes = propertyInfo.GetCustomAttributes<SwitchAttribute>().ToArray();
            _optionAttribute = propertyInfo.GetCustomAttribute<OptionAttribute>();
            _argumentAttribute = propertyInfo.GetCustomAttribute<ArgumentAttribute>();

            if (_optionAttribute == null && _argumentAttribute == null && !_switchAttributes.Any())
            {
                throw new ArgumentException($"The property {propertyInfo.DeclaringType}.{propertyInfo.Name} cannot be used as OptionProperty, it doesn't have the Option or the Argument attribute.");
            }

            DefaultValue = GetDefaultValue(propertyInfo);
            PropertyInfo = propertyInfo;
        }

        public Type Type
        {
            get => PropertyInfo.PropertyType;
        }

        public bool IsPositional
        {
            get => _argumentAttribute != null;
        }

        public int PositionalIndex
        {
            get
            {
                if (!IsPositional)
                {
                    throw new InvalidOperationException("Cannot get PositionIndex of an OptionProperty which is not positional.");
                }

                return _argumentAttribute.Index;
            }
        }

        public PropertyInfo PropertyInfo { get; }

        public bool IsOption
        {
            get => _optionAttribute != null;
        }

        public IReadOnlyList<string> OptionAliases
        {
            get
            {
                if (!IsOption)
                {
                    throw new InvalidOperationException("Cannot get PositionIndex of an OptionProperty which is not positional.");
                }

                return _optionAttribute.Aliases;
            }
        }

        public bool HasSwitches
        {
            get => _switchAttributes != null && _switchAttributes.Any();
        }

        public IReadOnlyList<ISwitch> Switches
        {
            get => _switchAttributes;
        }

        public object DefaultValue { get; }

        public bool HasDefaultValue
        {
            get => DefaultValue != null;
        }

        private static object GetDefaultValue(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();

            return attribute?.Value;
        }
    }
}