using System;

namespace LH.CommandLine.Options2.Values
{
    internal class FloatParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return float.Parse(rawValue);
        }
    }
}