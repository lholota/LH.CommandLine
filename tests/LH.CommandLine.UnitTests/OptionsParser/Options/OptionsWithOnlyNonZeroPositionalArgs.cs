namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public partial class WhenParsingUsingInvalidOptionsDefinition
    {
        public class OptionsWithOnlyNonZeroPositionalArgs
        {
            [Argument(1)]
            public string SomeArg { get; set; }
        }
    }
}