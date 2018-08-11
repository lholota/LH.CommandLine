using System;
using LH.CommandLine.Options;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithCustomValueParserWithInvalidCtor
    {
        [Option("option")]
        [ValueParser(typeof(InvalidValueParser))]
        public string Options { get; set; }

        public class InvalidValueParser : IValueParser<string>
        {
            public InvalidValueParser(string someMagic)
            {                
            }

            public string Parse(string rawValue)
            {
                throw new NotImplementedException();
            }
        }
    }
}
