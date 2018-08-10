using System;

namespace LH.CommandLine.Options.Values
{
    public class Int64Parser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return long.Parse(rawValue);
        }
    }
}