using System;

namespace LH.CommandLine.Options.Values
{
    internal class DecimalParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return decimal.Parse(rawValue);
        }
    }
}