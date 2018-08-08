using System;
using System.Collections.Generic;

namespace LH.CommandLine.Exceptions
{
    public class InvalidOptionsDefinitionException : Exception
    {
        public InvalidOptionsDefinitionException(Type optionsType, IReadOnlyList<string> errors)
            : base(CreateMessage(optionsType, errors))
        {
        }

        private static string CreateMessage(Type optionsType, IReadOnlyList<string> errors)
        {
            return $"The option type {optionsType} is invalid:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, errors);
        }
    }
}