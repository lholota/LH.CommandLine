using System;

namespace LH.CommandLine.Options.Values
{
    internal interface IValueParser
    {
        object Parse(string rawValue, Type targetType);
    }
}