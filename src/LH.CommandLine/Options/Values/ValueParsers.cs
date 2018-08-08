﻿using System;
using System.Collections.Generic;

namespace LH.CommandLine.Options.Values
{
    internal static class ValueParsers
    {
        private static readonly IDictionary<Type, IValueParser> Parsers = new Dictionary<Type, IValueParser>
        {
            { typeof(string), new StringParser() },
            { typeof(short), new Int16Parser() },
            { typeof(int), new Int32Parser() },
            { typeof(long), new Int64Parser() },
            { typeof(decimal), new DecimalParser() },
            { typeof(float), new FloatParser() },
            { typeof(double), new DoubleParser() }
        };

        public static IValueParser GetValueParser(Type targetType)
        {
            if (!Parsers.TryGetValue(targetType, out var parser))
            {
                throw new NotSupportedException($"Options/Arguments of type {targetType} are not supported.");
            }

            return parser;
        }
    }
}