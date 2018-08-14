using System;
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
            // TODO: Create collection (?)

            throw new NotImplementedException();
        }
    }
}