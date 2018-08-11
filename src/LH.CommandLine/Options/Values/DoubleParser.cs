using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    public class DoubleParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return double.Parse(rawValue);
        }
    }
}