using System;

namespace LH.CommandLine.Options.Values
{
    internal interface IValueParser
    {
        object Parse(string rawValue, Type targetType);
    }

    //public interface IValueParser<out T>
    //{
    //    T Parse(string rawValue);
    //}

    public abstract class ValueParserBase<T> : IValueParser
    {
        object IValueParser.Parse(string rawValue, Type targetType)
        {
            return Parse(rawValue);
        }

        public abstract T Parse(string rawValue);
    }
}