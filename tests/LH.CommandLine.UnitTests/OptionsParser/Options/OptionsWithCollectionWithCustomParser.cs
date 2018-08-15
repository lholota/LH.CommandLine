using LH.CommandLine.Options;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithCollectionWithCustomParser
    {
        [Option("strings")]
        [ValueParser(typeof(CustomParser))]
        public string[] Strings { get; set; }

        public class CustomParser : ValueParserBase<string>
        {
            public override string Parse(string rawValue)
            {
                return "CustomParsed:" + rawValue;
            }
        }
    }
}