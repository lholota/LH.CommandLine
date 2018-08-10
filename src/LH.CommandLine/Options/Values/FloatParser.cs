using System;

namespace LH.CommandLine.Options.Values
{
    public class FloatParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return float.Parse(rawValue);
        }
    }
}