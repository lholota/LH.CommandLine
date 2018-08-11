using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    internal class StringParser : IValueParser
    {
        public object Parse(string rawValue, Type targetType)
        {
            return rawValue;
        }
    }
}