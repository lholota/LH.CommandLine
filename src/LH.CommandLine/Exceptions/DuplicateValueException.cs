using System;

namespace LH.CommandLine.Exceptions
{
    internal class DuplicateValueException : Exception
    {
        public DuplicateValueException(string propertyName)
            : base($"The value for the property {propertyName} has already been set.")
        {
        }
    }
}
