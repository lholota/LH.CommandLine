namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public partial class WhenParsingUsingInvalidOptionsDefinition
    {
        public class OptionsWithSwitchSharingNameWithNamedOption
        {
            [Option("some-option")]
            public string PropertyA { get; set; }

            [Switch("some-option")]
            public string PropertyB { get; set; }
        }
    }
}