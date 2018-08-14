using System;

namespace LH.CommandLine.Options2.Values
{
    internal class DecimalParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return decimal.Parse(rawValue);
        }
    }
}