using System;

namespace LH.CommandLine.Options2.Values
{
    internal class StringParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return rawValue;
        }
    }
}