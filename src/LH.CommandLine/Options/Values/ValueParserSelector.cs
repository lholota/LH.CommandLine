using System;
using System.Reflection;
using LH.CommandLine.Exceptions;

namespace LH.CommandLine.Options.Values
{
    internal class ValueParserSelector
    {
        private readonly OptionsTypeDescriptor _typeDescriptor;
        private readonly IValueParserFactory _valueParserFactory;

        public ValueParserSelector(
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

        private IValueParser CreateExternalParser(Type parserType)
        {
            var factoryType = _valueParserFactory.GetType();
            var method = factoryType.GetMethod(nameof(IValueParserFactory.CreateParser));
            var generic = method.MakeGenericMethod(parserType);

            try
            {
                return (IValueParser)generic.Invoke(_valueParserFactory, null);
            }
            catch (TargetInvocationException ex)
            {
                throw new CreatingValueParserFailedException(factoryType, parserType, ex);
            }
        }
    }
}