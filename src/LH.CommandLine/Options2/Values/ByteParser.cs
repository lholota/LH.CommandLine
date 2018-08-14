using System;

namespace LH.CommandLine.Options2.Values
{
    internal class ByteParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return byte.Parse(rawValue);
        }
    }
}