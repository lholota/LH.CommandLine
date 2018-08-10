namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingPositionalArguments
    {
        private class OptionsWithPositional
        {
            [Argument(0)]
            public string Value1 { get; set; }

            [Argument(1)]
            public string Value2 { get; set; }
        }
    }
}