using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using LH.CommandLine.Options.Reflection;

namespace LH.CommandLine.Options.Descriptors
{
    internal class OptionProperty
    {
        public static IReadOnlyList<OptionProperty> GetPropertiesForType(Type type)
        {
            var result = new List<OptionProperty>();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in properties)
            {
                OptionProperty optionProperty;

                if (IsCollectionType(propertyInfo.PropertyType, out var itemType))
                {
                    optionProperty = new CollectionOptionProperty(propertyInfo, itemType);
                }
                else
                {
                    optionProperty = new OptionProperty(propertyInfo);
                }

                if (optionProperty.HasPositionalIndex || optionProperty.Switches.Any() ||
                    optionProperty.OptionAliases.Any())
                {
                    result.Add(optionProperty);
                }
            }

            return result;
        }

        internal OptionProperty(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;

            InitializeDefaultValue();
            InitializeSwitches();
            InitializePositionalIndex();
            InitializeNamedOptionAliases();
            InitializeCustomParserType();
        }

        public PropertyInfo PropertyInfo { get; }

        public Type Type
        {
            get => PropertyInfo.PropertyType;
        }

        public virtual Type ParsedType
        {
            get => Type;
        }

        public string Name
        {
            get => PropertyInfo.Name;
        }

        public bool HasDefaultValue { get; private set; }

        public object DefaultValue { get; private set; }

        public IReadOnlyDictionary<string, PropertyValue> Switches { get; private set; }

        public bool HasPositionalIndex { get; private set; }

        public int PositionalIndex { get; private set; }

        public IReadOnlyCollection<string> OptionAliases { get; private set; }

        public bool HasCustomParser { get; private set; }

        public Type CustomParserType { get; private set; }

        private void InitializeDefaultValue()
        {
            var defaultValueAttribute = PropertyInfo.GetCustomAttribute<DefaultValueAttribute>();

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

        private void InitializeSwitches()
        {
            var switches = new Dictionary<string, PropertyValue>();
            var switchAttributes = PropertyInfo.GetCustomAttributes<SwitchAttribute>();

            foreach (var switchAttribute in switchAttributes)
            {
                foreach (var alias in switchAttribute.Aliases)
                {
                    switches.Add(alias, new PropertyValue(this, switchAttribute.Value));
                }
            }

            Switches = switches;
        }

        private void InitializePositionalIndex()
        {
            var argumentAttribute = PropertyInfo.GetCustomAttribute<ArgumentAttribute>();

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

        private void InitializeNamedOptionAliases()
        {
            var result = new List<string>();
            var optionAttributes = PropertyInfo.GetCustomAttributes<OptionAttribute>();

            foreach (var optionAttribute in optionAttributes)
            {
                result.AddRange(optionAttribute.Aliases);
            }

            OptionAliases = result;
        }

        private void InitializeCustomParserType()
        {
            var attribute = PropertyInfo.GetCustomAttribute<ValueParserAttribute>();

            if (attribute != null)
            {
                CustomParserType = attribute.ParserType;
                HasCustomParser = true;
            }
            else
            {
                HasCustomParser = false;
            }
        }

        private static bool IsCollectionType(Type checkedType, out Type itemType)
        {
            if (checkedType.IsArray)
            {
                itemType = checkedType.GetElementType();
                return true;
            }

            itemType = null;
            return false;
        }
    }
}