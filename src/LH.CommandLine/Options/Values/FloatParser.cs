using System;

namespace LH.CommandLine.Options.Values
{
    internal class FloatParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return float.Parse(rawValue);
        }
    }
}