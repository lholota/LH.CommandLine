using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options
{
    internal class OptionCollectionValue : IOptionValue
    {
        private readonly OptionPropertyMetadata _propertyMetadata;
        private readonly IList _collectionItems;

        public OptionCollectionValue(OptionPropertyMetadata propertyMetadata)
        {
            _propertyMetadata = propertyMetadata;
            _collectionItems = new List<object>();
        }

        public void AddValue(object value)
        {
            _collectionItems.Add(value);
        }

        public object GetValue()
        {
            if (_collectionItems.Count == 0 && _propertyMetadata.HasDefaultValue)
            {
                return _propertyMetadata.DefaultValue;
            }

            if (_propertyMetadata.CollectionType == typeof(Array))
            {
                return CreateArray();
            }

            if (_propertyMetadata.CollectionType == typeof(Collection<>))
            {
                var collection = (IList)Activator.CreateInstance(_propertyMetadata.Type);

                foreach (var item in _collectionItems)
                {
                    collection.Add(item);
                }

                return collection;
            }

            if (_propertyMetadata.CollectionType == typeof(List<>))
            {
                var list = (IList)Activator.CreateInstance(_propertyMetadata.Type);

                foreach (var item in _collectionItems)
                {
                    list.Add(item);
                }

                return list;
            }

            throw new NotSupportedException($"The collection type {_propertyMetadata.CollectionType} is not supported.");
        }

        private object CreateArray()
        {
            var arrayType = _propertyMetadata.ParsedType.MakeArrayType();
            var array = (Array) Activator.CreateInstance(arrayType, _collectionItems.Count);

            for (var i = 0; i < _collectionItems.Count; i++)
            {
                array.SetValue(_collectionItems[i], i);
            }

            return array;
        }
    }
}