using LH.CommandLine.Options;
using LH.CommandLine.Exceptions;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingUsingInvalidOptionsDefinition
    {
        [Fact]
        public void ShouldThrowWhenMultipleOptionsHaveSameName()
        {
            var parser = new OptionsParser<OptionsWithTwoOptionsSharingName>();

            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        [Fact]
        public void ShouldThrowWhenMultipleOptionAndSwitchHaveSameName()
        {
            var parser = new OptionsParser<OptionsWithSwitchSharingNameWithOption>();

            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        private class OptionsWithTwoOptionsSharingName
        {
            [Option("some-option")]
            public string PropertyA { get; set; }

            [Switch("some-option")]
            public string PropertyB { get; set; }
        }

        private class OptionsWithSwitchSharingNameWithOption
        {
            [Option("some-option")]
            public string PropertyA { get; set; }

            [Switch("some-option")]
            public string PropertyB { get; set; }
        }
    }
}