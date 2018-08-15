using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingInvalidOptions
    {
        [Fact]
        public void ThrowWhenNamedOptionUsedAsSwitchWithFollowingOptions()
        {
            var parser = new OptionsParser<OptionsWithNamed>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--value", "--other", "some-value" }));
        }

        [Fact]
        public void ThrowWhenNamedOptionSpecifiedMultipleTimes()
        {
            var parser = new OptionsParser<OptionsWithNamed>();

            var exception = Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--value", "value1", "--value", "value2" }));

            Assert.Contains(exception.Errors, error => error.Contains("multiple"));
        }

        [Fact]
        public void ThrowWhenOptionIsUnknown()
        {
            var parser = new OptionsParser<OptionsWithNamed>();

            var exception = Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--some-unknown-option", "--value", "some-value" }));

            Assert.Contains(exception.Errors, error => error.Contains("some-unknown-option"));
        }

        private class OptionsWithNamed
        {
            [Option("value")]
            public string Value { get; set; }

            [Option("other")]
            public string OtherValue { get; set; }
        }
    }
}