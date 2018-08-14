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
            /*
             * - Create collection
             *   - Based on property type, find the generic collection type (e.g. List<>)
             *   - Create an instance of the type with ItemCollectionType as the generic parameter
             *   - Pass the list of the objects into the constructor
             *     - If not possible, loop and add them into the collection
             */

            // TODO: Create collection

            throw new NotImplementedException();
        }
    }
}