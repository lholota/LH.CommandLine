using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
using LH.CommandLine.UnitTests.OptionsParser.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingCollections
    {
        [Fact]
        public void ShouldParseIEnumerableOfIntegers()
        {
            var parser = new OptionsParser<OptionsWithIEnumerableOfInts>();
            var options = parser.Parse(new[] { "--numbers", "1", "2", "3" });

            Assert.Equal(new[] { 1, 2, 3 }, options.Numbers);
        }

        [Fact]
        public void ShouldParseArrayOfIntegers()
        {
            var parser = new OptionsParser<OptionsWithArrayOfInts>();
            var options = parser.Parse(new[] { "--numbers", "1", "2", "3" });

            Assert.Equal(new[] { 1, 2, 3 }, options.Numbers);
        }

        [Fact]
        public void ShouldParseReadOnlyCollectionOfIntegers()
        {
            var parser = new OptionsParser<OptionsWithReadOnlyCollectionOfInts>();
            var options = parser.Parse(new[] { "--numbers", "1", "2", "3" });

            Assert.Equal(new[] { 1, 2, 3 }, options.Numbers);
        }

        [Fact]
        public void ShouldParseCollectionOfIntegers()
        {
            var parser = new OptionsParser<OptionsWithCollectionOfInts>();
            var options = parser.Parse(new[] { "--numbers", "1", "2", "3" });

            Assert.Equal(new[] { 1, 2, 3 }, options.Numbers);
        }

        [Fact]
        public void ShouldParseICollectionOfIntegers()
        {
            var parser = new OptionsParser<OptionsWithICollectionOfInts>();
            var options = parser.Parse(new[] { "--numbers", "1", "2", "3" });

            Assert.Equal(new[] { 1, 2, 3 }, options.Numbers);
        }

        [Fact]
        public void ShouldParseReadOnlyListOfIntegers()
        {
            var parser = new OptionsParser<OptionsWithReadOnlyListOfInts>();
            var options = parser.Parse(new[] { "--numbers", "1", "2", "3" });

            Assert.Equal(new[] { 1, 2, 3 }, options.Numbers);
        }

        [Fact]
        public void ShouldParseIListOfIntegers()
        {
            var parser = new OptionsParser<OptionsWithIListOfInts>();
            var options = parser.Parse(new[] { "--numbers", "1", "2", "3" });

            Assert.Equal(new[] { 1, 2, 3 }, options.Numbers);
        }

        [Fact]
        public void ShouldParseListOfIntegers()
        {
            var parser = new OptionsParser<OptionsWithListOfInts>();
            var options = parser.Parse(new[] { "--numbers", "1", "2", "3" });

            Assert.Equal(new[] { 1, 2, 3 }, options.Numbers);
        }

        [Fact]
        public void ShouldParseCollectionWhenOptionsHaveDefaultValue()
        {
            var parser = new OptionsParser<OptionsWithCollectionWithDefaultValue>();
            var options = parser.Parse(new[] { "--strings", "A", "B" });

            Assert.Equal(new[] { "A", "B" }, options.Strings);
        }

        [Fact]
        public void ShouldParseCollectionWithCustomParser()
        {
            var parser = new OptionsParser<OptionsWithCollectionWithCustomParser>();
            var options = parser.Parse(new[] { "--strings", "A", "B" });

            Assert.Equal(new[] { "CustomParsed:A", "CustomParsed:B" }, options.Strings);
        }

        [Fact]
        public void ShouldThrowWhenCollectionOptionSpecifiedWithoutValue()
        {
            var parser = new OptionsParser<OptionsWithIEnumerableOfInts>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] {"--numbers"}));
        }

        [Fact]
        public void ShouldThrowIfCollectionValidationFails()
        {
            var parser = new OptionsParser<OptionsWithValidatedCollection>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--strings", "A" }));
        }

        [Fact]
        public void ShouldNotIncludeNextNamedOptionIntoCollection()
        {
            var parser = new OptionsParser<OptionsWithArrayOfIntsAndStringOption>();
            var options = parser.Parse(new[] { "--numbers", "1", "2", "3", "--string", "some-value" });

            Assert.Equal(new[] { 1, 2, 3 }, options.Numbers);
            Assert.Equal("some-value", options.String);
        }

        [Fact]
        public void ShouldThrowWhenMultipleSwitchesForTheSamePropertySpecified()
        {
            var parser = new OptionsParser<OptionsWithCollectionSwitches>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--one", "--two" }));
        }
    }
}