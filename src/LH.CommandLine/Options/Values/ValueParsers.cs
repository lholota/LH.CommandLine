using System;
using System.Collections.Generic;

namespace LH.CommandLine.Options.Values
{
    internal static class ValueParsers
    {
        private static readonly IDictionary<Type, IValueParser> Parsers = new Dictionary<Type, IValueParser>
        {
            { typeof(string), new StringParser() }
        };

        public static IValueParser GetValueParser(Type targetType)
        {
            if (!Parsers.TryGetValue(targetType, out var parser))
            {
                throw new NotSupportedException($"Options/Arguments of type {targetType} are not supported.");
            }

            return parser;
        }
    }
}