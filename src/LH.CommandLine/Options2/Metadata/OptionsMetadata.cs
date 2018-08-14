using System;
using System.Collections.Generic;
using System.Reflection;

namespace LH.CommandLine.Options2.Metadata
{
    internal class OptionsMetadata
    {
        public OptionsMetadata(Type optionsType)
        {
            Properties = GetProperties(optionsType);
        }

        public IReadOnlyList<OptionPropertyMetadata> Properties { get; }

        private IReadOnlyList<OptionPropertyMetadata> GetProperties(Type optionsType)
        {
            var result = new List<OptionPropertyMetadata>();
            var propInfos = optionsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propInfos)
            {
                if (OptionPropertyMetadata.TryCreateFromPropertyInfo(propertyInfo, out var propertyMetadata))
                {
                    result.Add(propertyMetadata);
                }
            }

            return result;
        }
    }
}