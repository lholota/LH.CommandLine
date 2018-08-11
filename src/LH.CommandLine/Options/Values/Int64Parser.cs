using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    public class Int64Parser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return long.Parse(rawValue);
        }
    }
}