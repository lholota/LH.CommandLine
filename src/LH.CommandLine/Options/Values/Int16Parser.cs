using System;

namespace LH.CommandLine.Options.Values
{
    internal class Int16Parser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return short.Parse(rawValue);
        }
    }
}
