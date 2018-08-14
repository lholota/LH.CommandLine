using System;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Extensions;

namespace LH.CommandLine.Options.Values
{
    internal class ActivatorValueParserFactory : Options2.Values.IValueParserFactory
    {
        public T CreateParser<T>()
        {
            var parserType = typeof(T);

            if (!parserType.HasParameterlessConstructor())
            {
                throw CreatingValueParserFailedException.CannotCreateInActivatorFactory(parserType);
            }

            return Activator.CreateInstance<T>();
        }
    }
}
