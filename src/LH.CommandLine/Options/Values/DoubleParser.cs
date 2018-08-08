using System;

namespace LH.CommandLine.Options.Values
{
    public class DoubleParser : IValueParser
    {
        public object Parse(string rawValue)
        {
            return double.Parse(rawValue);
        }
    }
}