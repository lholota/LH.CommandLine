using System;
using System.Reflection;
using LH.CommandLine.Exceptions;

namespace LH.CommandLine.Options2.Values
{
    internal class ValueParserSelector
    {
        private readonly IValueParserFactory _valueParserFactory;

        public ValueParserSelector(IValueParserFactory valueParserFactory)
        {
            _valueParserFactory = valueParserFactory;
        }

        public bool HasParserForProperty(OptionPropertyMetadata propertyMetadata)
        {
            return propertyMetadata.HasCustomParser
                   || DefaultParsers.HasParser(propertyMetadata.ParsedType);
        }

        public IValueParser GetParserForProperty(OptionPropertyMetadata propertyMetadata)
        {
            if (propertyMetadata.HasCustomParser)
            {
                return CreateExternalParser(propertyMetadata.CustomParserType);
            }

            return DefaultParsers.GetValueParser(propertyMetadata.ParsedType);
        }

        private IValueParser CreateExternalParser(Type parserType)
        {
            var factoryType = _valueParserFactory.GetType();
            var method = factoryType.GetMethod(nameof(IValueParserFactory.CreateParser));

            // ReSharper disable once PossibleNullReferenceException
            var generic = method.MakeGenericMethod(parserType);

            try
            {
                return (IValueParser) generic.Invoke(_valueParserFactory, null);
            }
            catch (TargetInvocationException ex) when (ex.InnerException is CreatingValueParserFailedException)
            {
                throw ex.InnerException;
            }
            catch (TargetInvocationException ex)
            {
                throw CreatingValueParserFailedException.CreatingFailed(factoryType, parserType, ex);
            }
        }
    }
}