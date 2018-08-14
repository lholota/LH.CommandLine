using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options2.Values
{
    public class Base64ByteArrayParser : ValueParserBase<byte[]>, Options.Values.IValueParser
    {
        object Options.Values.IValueParser.Parse(string rawValue, Type targetType)
        {
            return Parse(rawValue);
        }

        public override byte[] Parse(string rawValue)
        {
            return Convert.FromBase64String(rawValue);
        }
    }
}
