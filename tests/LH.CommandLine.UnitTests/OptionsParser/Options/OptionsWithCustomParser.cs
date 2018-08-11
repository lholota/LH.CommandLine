using System;
using LH.CommandLine.Options;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithCustomParser
    {
        [Option("string-option")]
        [ValueParser(typeof(CustomParser))]
        public string StringOption { get; set; }

        public class CustomParser : IValueParser<string>
        {
            private readonly string _value;

            public CustomParser()
                : this("MyCustomValue")
            {
            }

            public CustomParser(string value)
            {
                _value = value;
            }

            public string Parse(string rawValue)
            {
                return _value;
            }
        }
    }
}