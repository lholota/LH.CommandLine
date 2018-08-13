using System;
using System.Reflection;

namespace LH.CommandLine.Options.Reflection
{
    internal class CollectionOptionProperty : OptionProperty
    {
        internal CollectionOptionProperty(PropertyInfo propertyInfo, Type itemType)
            : base(propertyInfo)
        {
            ItemType = itemType;
        }

        public Type ItemType { get; }

        public override Type ParsedType
        {
            get => ItemType;
        }
    }
}