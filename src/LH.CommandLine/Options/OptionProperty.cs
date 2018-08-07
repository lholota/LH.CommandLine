using System;
using System.Collections.Generic;
using System.Reflection;

namespace LH.CommandLine.Options
{
    internal class OptionProperty
    {
        private readonly OptionAttribute _optionAttribute;
        private readonly ArgumentAttribute _argumentAttribute;

        internal OptionProperty(OptionAttribute optionAttribute, ArgumentAttribute argumentAttribute, PropertyInfo propertyInfo)
        {
            _optionAttribute = optionAttribute;
            _argumentAttribute = argumentAttribute;

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
            get => Type == typeof(bool) && IsNamed;
        }
    }
}