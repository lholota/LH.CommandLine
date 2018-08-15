using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options.Values
{
    internal class CollectionParser : ICollectionValueParser
    {
        private readonly IValueParser _innerParser;

        public CollectionParser(IValueParser innerParser)
        {
            _innerParser = innerParser;
        }

        public object Parse(string[] rawValues, OptionPropertyMetadata propertyMetadata)
        {
            var items = rawValues
                .Select(rawValue => _innerParser.Parse(rawValue, propertyMetadata.ParsedType))
                .ToArray();

            if (propertyMetadata.CollectionType == typeof(Array))
            {
                var arrayType = propertyMetadata.ParsedType.MakeArrayType();
                var array = (Array)Activator.CreateInstance(arrayType, items.Length);

                Array.Copy(items, array, items.Length);

                return array;
            }

            if (propertyMetadata.CollectionType == typeof(Collection<>))
            {
                var collection = (IList)Activator.CreateInstance(propertyMetadata.Type);

                foreach (var item in items)
                {
                    collection.Add(item);
                }

                return collection;
            }

            if (propertyMetadata.CollectionType == typeof(List<>))
            {
                var list = (IList)Activator.CreateInstance(propertyMetadata.Type);

                foreach (var item in items)
                {
                    list.Add(item);
                }

                return list;
            }

            throw new NotSupportedException($"The collection type {propertyMetadata.CollectionType} is not supported.");
        }
    }
}