using System;

namespace LH.CommandLine.Exceptions
{
    public class CreatingValueParserFailedException : Exception
    {
        public static CreatingValueParserFailedException CannotCreateInActivatorFactory(Type parserType)
        {
            return new CreatingValueParserFailedException(
                $"Cannot create value parser type {parserType}. The default factory only supports classes with parameterless constructor. If you want to use more complex parser, please pass your implementation of IValueParserFactory into the OptionsParser.");
        }

        public static CreatingValueParserFailedException CreatingFailed(Type factoryType, Type parserType, Exception innerException)
        {
            return new CreatingValueParserFailedException(
                $"The factory {factoryType} failed to create an instace of {parserType}.",
                innerException);
        }

        private CreatingValueParserFailedException(string message, Exception innerException = null) 
            : base(message, innerException)
        {
        }
    }
}