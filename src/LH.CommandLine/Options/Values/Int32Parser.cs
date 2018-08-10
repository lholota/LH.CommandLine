using System;

namespace LH.CommandLine.Options.Values
{
    public class Int32Parser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return int.Parse(rawValue);
        }
    }
}