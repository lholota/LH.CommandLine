using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    public class FloatParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return float.Parse(rawValue);
        }
    }
}