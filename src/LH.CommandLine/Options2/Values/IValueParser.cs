using System;

namespace LH.CommandLine.Options2.Values
{
    internal interface IValueParser
    {
        object Parse(string rawValue, Type targetType);
    }
}