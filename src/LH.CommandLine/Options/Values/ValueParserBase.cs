using System;

namespace LH.CommandLine.Options.Values
{
    public abstract class ValueParserBase<T> : IValueParser
    {
        object IValueParser.Parse(string rawValue, Type targetType)
        {
            return Parse(rawValue);
        }

        public abstract T Parse(string rawValue);
    }
}