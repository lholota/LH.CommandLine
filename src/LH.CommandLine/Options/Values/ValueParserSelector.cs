using System;
using System.Reflection;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Extensions;
using LH.CommandLine.Options.Reflection;

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
            if (_typeDescriptor.TryFindValueParserOverrideType(propertyInfo, out Type externalParserType))
            {
                return CreateExternalParser(externalParserType);
            }

            if (propertyInfo.PropertyType.IsCollection(out var itemType))
            {
                return DefaultParsers.GetValueParser(itemType);
            }

            return DefaultParsers.GetValueParser(propertyInfo.PropertyType);
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