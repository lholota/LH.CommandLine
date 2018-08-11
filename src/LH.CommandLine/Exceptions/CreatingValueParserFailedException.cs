using System;

namespace LH.CommandLine.Exceptions
{
    public class CreatingValueParserFailedException : Exception
    {
        public CreatingValueParserFailedException(Type factoryType, Type parserType, Exception innerException) 
            : base($"The factory {factoryType} failed to create an instace of {parserType}.", innerException)
        {
        }
    }
}