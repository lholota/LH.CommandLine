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
    }
}