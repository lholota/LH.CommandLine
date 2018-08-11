using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    public class Base64ByteArrayParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return Convert.FromBase64String(rawValue);
        }
    }
}
