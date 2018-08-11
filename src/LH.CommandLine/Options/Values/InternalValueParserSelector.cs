using System;
using System.Reflection;

namespace LH.CommandLine.Options.Values
{
    internal class InternalValueParserSelector
    {
        private readonly OptionsTypeDescriptor _typeDescriptor;
        private readonly IValueParserFactory _valueParserFactory;

        public InternalValueParserSelector(
            OptionsTypeDescriptor typeDescriptor,
            IValueParserFactory valueParserFactory)
        {
            _typeDescriptor = typeDescriptor;
            _valueParserFactory = valueParserFactory;
        }

        public bool HasParserForProperty(PropertyInfo propertyInfo)
        {
            return GetParserForProperty(propertyInfo) != null;
        }

        public IValueParser GetParserForProperty(PropertyInfo propertyInfo)
        {
            if (_typeDescriptor.TryFindValueParserOverrideType(propertyInfo, out var parserType))
            {
                return CreateExternalParser(parserType);
            }

            return DefaultParsers.GetValueParser(propertyInfo.PropertyType);
        }

        private IValueParser CreateExternalParser(Type type)
        {
            
        }
    }
}