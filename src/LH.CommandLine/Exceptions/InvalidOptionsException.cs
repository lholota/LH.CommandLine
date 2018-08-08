using System;
using System.Collections.Generic;

namespace LH.CommandLine.Exceptions
{
    public class InvalidOptionsException : Exception
    {
        public InvalidOptionsException(IReadOnlyList<string> errors)
            : base("Invalid options")
        {
            Errors = errors;
        }
        
        public IReadOnlyList<string> Errors { get; }
    }
}