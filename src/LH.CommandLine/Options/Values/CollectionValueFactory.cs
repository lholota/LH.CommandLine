using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options.Values
{
    internal class CollectionValueFactory
    {
        public object CreateCollection(IReadOnlyList<object> items, OptionPropertyMetadata propertyMetadata)
        {
            if (propertyMetadata.CollectionType == typeof(Array))
            {
                var arrayType = propertyMetadata.ParsedType.MakeArrayType();
                var array = (Array)Activator.CreateInstance(arrayType, items.Count);

                for (int i = 0; i < items.Count; i++)
                {
                    array.SetValue(items[i], i);
                }

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