using System;

namespace LH.CommandLine.Exceptions
{
    public class InvalidOptionsDefinitionException : Exception
    {
        public InvalidOptionsDefinitionException(string message)
            : base(message)
        {
            
        }
    }
}
