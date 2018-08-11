using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingBoolValues
    {
        [Theory]
        [InlineData("True", true)]
        [InlineData("TRUE", true)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("1", true)]
        [InlineData("0", false)]
        public void ShouldParseValue(string argsValue, bool expectedValue)
        {
            var parser = new OptionsParser<BoolOptions>();
            var options = parser.Parse(new[] { "--bool-value", argsValue });

            Assert.Equal(expectedValue, options.BoolValue);
        }

        [Theory]
        [InlineData("invalid-value")]
        [InlineData("2")]
        public void ShouldFailWhenValueInvalid(string argValue)
        {
            var parser = new OptionsParser<BoolOptions>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--bool-value", argValue }));
        }

        private class BoolOptions
        {
            [Option("bool-value")]
            public bool BoolValue { get; set; }
        }
    }
}