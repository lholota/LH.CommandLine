using System;
using LH.CommandLine.Options2.Values;

namespace LH.CommandLine.Options2
{
    internal class CollectionOptionProperty : OptionProperty
    {
        public CollectionOptionProperty(OptionPropertyMetadata propertyMetadata, ValueParserSelector parserSelector) 
            : base(propertyMetadata, parserSelector)
        {
        }

        public override object GetValue()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateValue(object value)
        {
            throw new NotImplementedException();
        }
    }
}