using System;

namespace LH.CommandLine.Options2.Values
{
    internal class DoubleParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return double.Parse(rawValue);
        }
    }
}