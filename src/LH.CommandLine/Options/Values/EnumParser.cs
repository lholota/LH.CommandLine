using System;
using System.Linq;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    internal class EnumParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            if (TryParseUnderlyingTypeValue(rawValue, targetType, out var parsedValue))
            {
                return parsedValue;
            }

            if (!IsValidValue(targetType, rawValue))
            {
                throw new FormatException($"The value '{rawValue}' is not a valid enumeration value for the type {targetType}.");
            }

            return Enum.Parse(targetType, rawValue, true);
        }

        private bool IsValidValue(Type targetType, string rawValue)
        {
            var validValues = Enum.GetNames(targetType);

            return validValues.Any(x => string.Equals(x, rawValue, StringComparison.OrdinalIgnoreCase));
        }

        private bool TryParseUnderlyingTypeValue(string rawValue, Type targetType, out object parsedValue)
        {
            var underlyingType = targetType.GetEnumUnderlyingType();
            var underlyingTypeParser = DefaultParsers.GetValueParser(underlyingType);

            try
            {
                var numericValue = underlyingTypeParser.Parse(rawValue, targetType);

                if (Enum.IsDefined(targetType, numericValue))
                {
                    parsedValue = Enum.ToObject(targetType, numericValue);
                    return true;
                }
            }
            catch (Exception) { }

            parsedValue = null;
            return false;
        }
    }
}