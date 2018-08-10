using System;

namespace LH.CommandLine.Options.Values
{
    public class DecimalParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return decimal.Parse(rawValue);
        }
    }
}