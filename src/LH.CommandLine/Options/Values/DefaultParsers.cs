using System;
using System.Collections.Generic;
using LH.CommandLine.Options.BuiltinParsers;

namespace LH.CommandLine.Options.Values
{
    internal static class DefaultParsers
    {
        private static readonly EnumParser EnumParser = new EnumParser();

        private static readonly IDictionary<Type, IValueParser> Parsers = new Dictionary<Type, IValueParser>
        {
            { typeof(string), new StringParser() },
            { typeof(byte), new ByteParser() },
            { typeof(short), new Int16Parser() },
            { typeof(int), new Int32Parser() },
            { typeof(long), new Int64Parser() },
            { typeof(decimal), new DecimalParser() },
            { typeof(float), new FloatParser() },
            { typeof(double), new DoubleParser() },
            { typeof(bool), new BoolParser() },
            { typeof(byte[]), new Base64ByteArrayParser() }
        };

        public static IValueParser GetValueParser(Type targetType)
        {
            if (targetType.IsEnum)
            {
                return EnumParser;
            }

            if (!Parsers.TryGetValue(targetType, out var parser))
            {
                throw new NotSupportedException($"Options/Arguments of type {targetType} are not supported.");
            }

            return parser;
        }
    }
}