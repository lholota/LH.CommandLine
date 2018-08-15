using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingPositionalArguments
    {
        [Theory]
        [InlineData(new[] { "some-value", "529" }, "some-value", 529)]
        [InlineData(new[] { "some-value" }, "some-value", 0)]
        public void ShouldParsePositionalArguments(string[] args, string expectedValue1, int expectedValue2)
        {
            var parser = new OptionsParser<OptionsWithPositional>();
            var options = parser.Parse(args);

            Assert.Equal(expectedValue1, options.Value1);
            Assert.Equal(expectedValue2, options.Value2);
        }

        [Theory]
        [InlineData(new[] { "str-value", "529", "655", "722" }, "str-value", new[] { 529, 655, 722 })]
        public void ShouldParseCollectionPositionalArguments(string[] args, string expectedStringValue, int[] expectedNumberCollection)
        {
            var parser = new OptionsParser<OptionsWithPositionalCollection>();
            var options = parser.Parse(args);

            Assert.Equal(expectedStringValue, options.StringValue);
            Assert.Equal(expectedNumberCollection, options.Numbers);
        }

        private class OptionsWithPositional
        {
            [Argument(0)]
            public string Value1 { get; set; }

            [Argument(1)]
            public int Value2 { get; set; }
        }

        private class OptionsWithPositionalCollection
        {
            [Argument(0)]
            public string StringValue { get; set; }

            [Argument(1)]
            public int[] Numbers { get; set; }
        }
    }
}