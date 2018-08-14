using System;
using System.Collections.Generic;
using System.Linq;

namespace LH.CommandLine.Options.Metadata
{
    internal class OptionsMetadata
    {
        public OptionsMetadata(Type optionsType)
        {
            OptionsType = optionsType;
            Properties = OptionPropertyMetadata.GetPropertiesForType(optionsType);
        }

        public Type OptionsType { get; }

        public IReadOnlyList<OptionPropertyMetadata> Properties { get; }

        public bool TryGetSwitchValueByName(string name, out PropertyValue switchValue)
        {
            foreach (var optionPropertyMetadata in Properties)
            {
                if (optionPropertyMetadata.Switches.TryGetValue(name, out switchValue))
                {
                    return true;
                }
            }

            switchValue = null;
            return false;
        }

        public bool TryGetPropertyByIndex(int index, out OptionPropertyMetadata property)
        {
            property = Properties.SingleOrDefault(x => x.HasPositionalIndex && x.PositionalIndex == index);

            return property != null;
        }

        public bool TryGetPropertyByOptionName(string name, out OptionPropertyMetadata property)
        {
            property = Properties.SingleOrDefault(x => x.OptionAliases.Contains(name));

            return property != null;
        }

        public bool IsKeyword(string name)
        {
            return TryGetSwitchValueByName(name, out _)
                   || TryGetPropertyByOptionName(name, out _);
        }
    }
}