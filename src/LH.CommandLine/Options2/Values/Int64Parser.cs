using System;

namespace LH.CommandLine.Options2.Values
{
    internal class Int64Parser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return long.Parse(rawValue);
        }
    }
}