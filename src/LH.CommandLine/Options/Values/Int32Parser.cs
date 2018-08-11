using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    public class Int32Parser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return int.Parse(rawValue);
        }
    }
}