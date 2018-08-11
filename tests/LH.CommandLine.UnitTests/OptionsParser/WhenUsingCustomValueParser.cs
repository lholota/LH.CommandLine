using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
using LH.CommandLine.UnitTests.OptionsParser.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenUsingCustomValueParser
    {
        [Fact]
        public void ShouldCreateAndCallParser()
        {
            var parser = new OptionsParser<OptionsWithCustomParser>();
            var options = parser.Parse(new []{ "--string-option", "some-value" });

            Assert.Equal("MyCustomValue", options.StringOption);
        }

        [Fact]
        public void ShouldThrowWhenCustomValueParserReturnsInvalidValue()
        {
            var parser = new OptionsParser<OptionsWithCustomParserWithInvalidValue>();

            Assert.Throws<InvalidOptionsDefinitionException>(
                () => parser.Parse(new[] { "--string-option", "some-value" }));
        }
    }
}