using System.ComponentModel;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithCollectionWithDefaultValue
    {
        [Option("strings")]
        [DefaultValue(new[] { "Default" })]
        public string[] Strings { get; set; }
    }
}