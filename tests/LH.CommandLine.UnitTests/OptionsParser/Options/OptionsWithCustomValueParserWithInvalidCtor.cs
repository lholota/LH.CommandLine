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

        public class InvalidValueParser : ValueParserBase<string>
        {
            public InvalidValueParser(string someMagic)
            {                
            }

            public override string Parse(string rawValue)
            {
                throw new NotImplementedException();
            }
        }
    }
}