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

        public bool TryGetSwitchPropertyByName(string name, out OptionProperty optionProperty)
        {
            optionProperty = _properties
                .SingleOrDefault(x => x.IsNamed && x.IsSwitch && x.Aliases.Contains(name));

            return optionProperty != null;
        }

        public bool TryGetPropertyByName(string name, out OptionProperty optionProperty)
        {
            optionProperty = _properties
                .SingleOrDefault(x => x.IsNamed && x.Aliases.Contains(name));

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
