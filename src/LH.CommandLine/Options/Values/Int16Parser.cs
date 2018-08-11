using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    public class Int16Parser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return short.Parse(rawValue);
        }
    }
}
