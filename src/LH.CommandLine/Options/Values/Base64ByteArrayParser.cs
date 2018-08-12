using System;

namespace LH.CommandLine.Options.Values
{
    public class Base64ByteArrayParser : ValueParserBase<byte[]>, IValueParser
    {
        object IValueParser.Parse(string rawValue, Type targetType)
        {
            return Parse(rawValue);
        }

        public override byte[] Parse(string rawValue)
        {
            return Convert.FromBase64String(rawValue);
        }
    }
}
