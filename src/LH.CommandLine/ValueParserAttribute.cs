using System;

namespace LH.CommandLine
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValueParserAttribute : Attribute
    {
        public ValueParserAttribute(Type parserType)
        {
            ParserType = parserType;
        }

        public Type ParserType { get; }
    }
}