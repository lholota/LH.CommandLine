using System;

namespace LH.CommandLine.Options.Values
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
