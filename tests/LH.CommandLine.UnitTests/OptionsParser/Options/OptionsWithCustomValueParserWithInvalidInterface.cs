using System;
using LH.CommandLine.Options;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithCustomValueParserWithInvalidInterface
    {
        [Option("option")]
        [ValueParser(typeof(InvalidValueParser))]
        public string Options { get; set; }

        public class InvalidValueParser
        { 
        }
    }
}