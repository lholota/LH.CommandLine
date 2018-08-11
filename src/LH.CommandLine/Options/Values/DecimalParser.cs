using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    public class DecimalParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return decimal.Parse(rawValue);
        }
    }
}