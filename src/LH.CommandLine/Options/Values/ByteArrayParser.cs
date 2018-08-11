using System;

namespace LH.CommandLine.Options.Values
{
    public class ByteArrayParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return Convert.FromBase64String(rawValue);
        }
    }
}