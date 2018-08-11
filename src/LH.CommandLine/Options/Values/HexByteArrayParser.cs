using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    public class HexByteArrayParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return ConvertFromHexString(rawValue);
        }

        private byte[] ConvertFromHexString(string rawValue)
        {
            if (rawValue.Length % 2 != 0)
            {
                throw new FormatException("Invalid hex string length.");
            }

            var index = 0;
            var byteIndex = 0;

            if (rawValue.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                index += 2;
            }

            var bytes = new byte[(rawValue.Length - index) / 2];

            while (index < rawValue.Length)
            {
                bytes[byteIndex] = Convert.ToByte(rawValue.Substring(index, 2), 16);

                index += 2;
                byteIndex++;
            }

            return bytes;
        }
    }
}
