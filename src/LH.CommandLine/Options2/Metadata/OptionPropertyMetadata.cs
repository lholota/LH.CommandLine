using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using LH.CommandLine.Options;

namespace LH.CommandLine.Options2
{
    internal class OptionPropertyMetadata
    {
        public static bool TryCreateFromPropertyInfo(PropertyInfo propInfo, out OptionPropertyMetadata property)
        {
            property = new OptionPropertyMetadata(propInfo);

            if (!property.HasPositionalIndex && !property.Switches.Any() && !property.OptionAliases.Any())
            {
                property = null;
                return false;
            }

            return true;
        }

        private readonly PropertyInfo _propertyInfo;

        private OptionPropertyMetadata(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;

            InitializeDefaultValue(propertyInfo);
            InitializeCustomParserType(propertyInfo);
            InitializeSwitches(propertyInfo);
            InitializePositionalIndex(propertyInfo);
            InitializeOptionAliases(propertyInfo);
            InitializeCollection(propertyInfo);
        }

        public bool HasDefaultValue { get; private set; }

        public object DefaultValue { get; private set; }

        public bool HasCustomParser { get; private set; }

        public Type CustomParserType { get; private set; }

        public bool HasPositionalIndex { get; private set; }

        public int PositionalIndex { get; private set; }

        public bool IsCollection { get; private set; }

        public Type CollectionItemType { get; private set; }

        public IReadOnlyDictionary<string, object> Switches { get; private set; }

        public IReadOnlyList<string> OptionAliases { get; private set; }

        public Type ParsedType
        {
            get
            {
                if (IsCollection)
                {
                    return CollectionItemType;
                }

                return PropertyType;
            }
        }

        public Type PropertyType
        {
            get => _propertyInfo.PropertyType;
        }

        public string PropertyName
        {
            get => _propertyInfo.Name;
        }

        private void InitializeDefaultValue(PropertyInfo propertyInfo)
        {
            var defaultValueAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();

            if (defaultValueAttribute != null)
            {
                DefaultValue = defaultValueAttribute.Value;
                HasDefaultValue = true;
            }
            else
            {
                HasDefaultValue = false;
            }
        }

        private void InitializeCustomParserType(PropertyInfo propertyInfo)
        {
            var valueParserAttribute = propertyInfo.GetCustomAttribute<ValueParserAttribute>();

            if (valueParserAttribute != null)
            {
                HasCustomParser = true;
                CustomParserType = valueParserAttribute.ParserType;
            }
            else
            {
                HasCustomParser = false;
            }
        }

        private void InitializePositionalIndex(PropertyInfo propertyInfo)
        {
            var argumentAttribute = propertyInfo.GetCustomAttribute<ArgumentAttribute>();

            if (argumentAttribute != null)
            {
                PositionalIndex = argumentAttribute.Index;
                HasPositionalIndex = true;
            }
            else
            {
                HasPositionalIndex = false;
            }
        }

        private void InitializeSwitches(PropertyInfo propertyInfo)
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

            Switches = switches;
        }

        private void InitializeOptionAliases(PropertyInfo propertyInfo)
        {
            OptionAliases = propertyInfo.GetCustomAttributes<OptionAttribute>()
                .SelectMany(x => x.Aliases)
                .ToArray();
        }

        private void InitializeCollection(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsArray)
            {
                IsCollection = true;
                CollectionItemType = propertyInfo.PropertyType.GetElementType();
                return;
            }

            IsCollection = false;
        }
    }
}