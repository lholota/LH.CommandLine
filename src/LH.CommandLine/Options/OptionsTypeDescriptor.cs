﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace LH.CommandLine.Options
{
    internal class OptionsTypeDescriptor
    {
        private readonly IDictionary<string, PropertyValue> _switchValues;
        private readonly IDictionary<int, PropertyInfo> _positionalProperties;
        private readonly IDictionary<string, PropertyInfo> _namedOptionPropertiesLookup;

        public OptionsTypeDescriptor(Type optionsType)
        {
            OptionsType = optionsType;

            Properties = optionsType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            DefaultValues = GetDefaultValues(Properties);

            _switchValues = CreateSwitchValuesLookup(Properties);
            _positionalProperties = CreatePositionalPropertiesLookup(Properties);
            _namedOptionPropertiesLookup = CreateNamedOptionsPropertiesLookup(Properties);
        }

        public Type OptionsType { get; }

        public IReadOnlyCollection<PropertyInfo> Properties { get; }

        public IReadOnlyCollection<PropertyValue> DefaultValues { get; }

        public bool TryFindSwitchValue(string name, out PropertyValue switchValue)
        {
            return _switchValues.TryGetValue(name, out switchValue);
        }

        public bool TryFindPropertyByPositionalIndex(int index, out PropertyInfo propertyInfo)
        {
            return _positionalProperties.TryGetValue(index, out propertyInfo);
        }

        public bool TryFindPropertyByOptionName(string name, out PropertyInfo propertyInfo)
        {
            return _namedOptionPropertiesLookup.TryGetValue(name, out propertyInfo);
        }

        public IEnumerable<string> GetAliases()
        {
            return _switchValues.Keys.Concat(_namedOptionPropertiesLookup.Keys);
        }

        public IEnumerable<PropertyValue> GetSwitchValues()
        {
            return _switchValues.Select(x => x.Value);
        }

        private IDictionary<string, PropertyValue> CreateSwitchValuesLookup(IReadOnlyCollection<PropertyInfo> properties)
        {
            var result = new Dictionary<string, PropertyValue>();

            foreach (var propertyInfo in properties)
            {
                var switchAttributes = propertyInfo.GetCustomAttributes<SwitchAttribute>();

                foreach (var switchAttribute in switchAttributes)
                {
                    foreach (var alias in switchAttribute.Aliases)
                    {
                        result.Add(alias, new PropertyValue(propertyInfo, switchAttribute.Value));
                    }
                }
            }

            return result;
        }

        private IDictionary<int, PropertyInfo> CreatePositionalPropertiesLookup(IReadOnlyCollection<PropertyInfo> properties)
        {
            var result = new Dictionary<int, PropertyInfo>();

            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<ArgumentAttribute>();

                if (attribute != null)
                {
                    result.Add(attribute.Index, propertyInfo);
                }
            }

            return result;
        }

        private IDictionary<string, PropertyInfo> CreateNamedOptionsPropertiesLookup(IReadOnlyCollection<PropertyInfo> properties)
        {
            var result = new Dictionary<string, PropertyInfo>();

            foreach (var propertyInfo in properties)
            {
                var optionAttributes = propertyInfo.GetCustomAttributes<OptionAttribute>();

                foreach (var optionAttribute in optionAttributes)
                {
                    foreach (var alias in optionAttribute.Aliases)
                    {
                        result.Add(alias, propertyInfo);
                    }
                }
            }

            return result;
        }

        private IReadOnlyCollection<PropertyValue> GetDefaultValues(IReadOnlyCollection<PropertyInfo> properties)
        {
            var result = new List<PropertyValue>();

            foreach (var propertyInfo in properties)
            {
                var defaultValueAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();

                if (defaultValueAttribute != null)
                {
                    result.Add(new PropertyValue(propertyInfo, defaultValueAttribute.Value));
                }
            }

            return result;
        }
    }
}