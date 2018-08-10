using System;

namespace LH.CommandLine.Options.Values
{
    internal class StringParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return rawValue;
        }
    }
}