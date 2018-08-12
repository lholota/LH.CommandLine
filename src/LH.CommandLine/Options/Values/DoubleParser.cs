using System;

namespace LH.CommandLine.Options.Values
{
    internal class DoubleParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return double.Parse(rawValue);
        }
    }
}