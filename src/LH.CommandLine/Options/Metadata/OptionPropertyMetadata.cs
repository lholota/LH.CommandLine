using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace LH.CommandLine.Options.Metadata
{
    internal class OptionPropertyMetadata
    {
        public static IReadOnlyList<OptionPropertyMetadata> GetPropertiesForType(Type type)
        {
            var result = new List<OptionPropertyMetadata>();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in properties)
            {
                var propertyMetadata = new OptionPropertyMetadata(propertyInfo);

                if (propertyMetadata.HasPositionalIndex 
                    || propertyMetadata.Switches.Any() 
                    || propertyMetadata.OptionAliases.Any())
                {
                    result.Add(propertyMetadata);
                }
            }

            return result;
        }

        private OptionPropertyMetadata(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;

            InitializeDefaultValue();
            InitializeSwitches();
            InitializePositionalIndex();
            InitializeNamedOptionAliases();
            InitializeCustomParserType();
            InitializeCollection();
        }

        public PropertyInfo PropertyInfo { get; }

        public Type Type
        {
            get => PropertyInfo.PropertyType;
        }

        public virtual Type ParsedType
        {
            get
            {
                if (IsCollection)
                {
                    return CollectionItemType;
                }

                return Type;
            }
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

        public bool IsCollection { get; private set; }

        public Type CollectionItemType { get; private set; }

        public Type CollectionType { get; private set; }

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

        private void InitializeCollection()
        {
            if (PropertyInfo.PropertyType == typeof(byte[]))
            {
                IsCollection = false;
                return;
            }

            if (PropertyInfo.PropertyType.IsArray)
            {
                IsCollection = true;
                CollectionType = typeof(Array);
                CollectionItemType = PropertyInfo.PropertyType.GetElementType();

                return;
            }

            if (PropertyInfo.PropertyType.IsConstructedGenericType)
            {
                var genericType = PropertyInfo.PropertyType.GetGenericTypeDefinition();
                var genericArgs = PropertyInfo.PropertyType.GetGenericArguments();

                if (genericType == typeof(IList<>)
                    || genericType == typeof(IEnumerable<>)
                    || genericType == typeof(IReadOnlyList<>)
                    || genericType == typeof(ICollection<>)
                    || genericType == typeof(IReadOnlyCollection<>))
                {
                    IsCollection = true;
                    CollectionItemType = genericArgs[0];
                    CollectionType = typeof(Array);

                    return;
                }

                if (genericType == typeof(List<>))
                {
                    IsCollection = true;
                    CollectionItemType = genericArgs[0];
                    CollectionType = typeof(List<>);
                }

                if (genericType == typeof(Collection<>))
                {
                    IsCollection = true;
                    CollectionItemType = genericArgs[0];
                    CollectionType = typeof(Collection<>);
                }
            }
        }
    }
}