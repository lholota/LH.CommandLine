using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    internal class BoolParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            if (byte.TryParse(rawValue, out byte byteValue))
            {
                switch (byteValue)
                {
                    case 0:
                        return false;

                    case 1:
                        return true;
                }
            }

            return bool.Parse(rawValue);
        }
    }
}
