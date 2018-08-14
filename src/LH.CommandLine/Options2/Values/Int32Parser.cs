using System;

namespace LH.CommandLine.Options2.Values
{
    internal class Int32Parser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return int.Parse(rawValue);
        }
    }
}