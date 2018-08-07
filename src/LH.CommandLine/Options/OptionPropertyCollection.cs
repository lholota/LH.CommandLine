using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LH.CommandLine.Options
{
    internal class OptionPropertyCollection : IEnumerable<OptionProperty>
    {
        private readonly IReadOnlyList<OptionProperty> _properties;

        public OptionPropertyCollection(Type type)
        {
            _properties = GetOptionPropertiesFromType(type);
        }

        public bool TryGetSwitchPropertyByName(string name, out OptionProperty optionProperty, out object switchValue)
        {
            foreach (var property in _properties)
            {
                foreach (var propertySwitch in property.Switches)
                {
                    if (propertySwitch.Aliases.Contains(name))
                    {
                        optionProperty = property;
                        switchValue = propertySwitch.Value;

                        return true;
                    }
                }
            }

            optionProperty = null;
            switchValue = null;

            return false;
        }

        public bool TryGetPropertyByName(string name, out OptionProperty optionProperty)
        {
            optionProperty = _properties
                .SingleOrDefault(x => x.IsOption && x.OptionAliases.Contains(name));

            return optionProperty != null;
        }

        public bool TryGetPropertyByIndex(int index, out OptionProperty optionProperty)
        {
            optionProperty = _properties
                .SingleOrDefault(x => x.IsPositional && x.PositionalIndex == index);

            return optionProperty != null;
        }

        private static IReadOnlyList<OptionProperty> GetOptionPropertiesFromType(Type type)
        {
            var optionProperties = new List<OptionProperty>();
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var propertyInfo in propertyInfos)
            {
                if (OptionProperty.IsOptionProperty(propertyInfo))
                {
                    optionProperties.Add(new OptionProperty(propertyInfo));
                }
            }

            return optionProperties;
        }

        public IEnumerator<OptionProperty> GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
