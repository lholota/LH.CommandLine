using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    internal class ByteParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return byte.Parse(rawValue);
        }
    }
}