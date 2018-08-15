using LH.CommandLine.Options;
using LH.CommandLine.Exceptions;
using LH.CommandLine.UnitTests.OptionsParser.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingUsingInvalidOptionsMetadata
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
            var parser = new OptionsParser<WhenParsingUsingInvalidOptionsDefinition.OptionsWithSwitchSharingNameWithNamedOption>();

            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        [Fact]
        public void ShouldThrowWhenDefaultValueIsOfDifferentTypeThanProperty()
        {
            var parser = new OptionsParser<OptionsWithInvalidDefaultValueType>();

            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        [Fact]
        public void ShouldThrowWhenSwitchValueOfDifferentTypeThanProperty()
        {
            var parser = new OptionsParser<OptionsWithInvalidSwitchValue>();

            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        [Fact]
        public void ShouldThrowWhenPositionalArgsIndexDontStartWithZero()
        {
            var parser = new OptionsParser<WhenParsingUsingInvalidOptionsDefinition.OptionsWithOnlyNonZeroPositionalArgs>();
            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        [Fact]
        public void ShouldThrowWhenPositionalArgsIndexesAreNotContinous()
        {
            var parser = new OptionsParser<OptionsWithNonContinousPositionalIndexes>();
            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        [Fact]
        public void ShouldThrowWhenCustomValueParserDoesNotImplementInterface()
        {
            var parser = new OptionsParser<OptionsWithCustomValueParserWithInvalidInterface>();
            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        [Fact]
        public void ShouldThrowWhenCollectionIsNotLastPositional()
        {
            var parser = new OptionsParser<OptionsWithCollectionPositionalWhichIsNotLast>();
            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        [Fact]
        public void ShouldThrowWhenMultipleCollectionsArePositions()
        {
            var parser = new OptionsParser<OptionsWithMultiplePositionalCollections>();
            Assert.Throws<InvalidOptionsDefinitionException>(() => parser.Parse(new string[0]));
        }

        private class OptionsWithInvalidSwitchValue
        {
            [Switch("some-switch", Value = 32)]
            public string PropertyA { get; set; }
        }

        private class OptionsWithTwoOptionsSharingName
        {
            [Option("some-option")]
            public string PropertyA { get; set; }

            [Switch("some-option")]
            public string PropertyB { get; set; }
        }

        private class OptionsWithNonContinousPositionalIndexes
        {
            [Argument(0)]
            public string SomeArg0 { get; set; }

            [Argument(1)]
            public string SomeArg1 { get; set; }

            [Argument(5)]
            public string SomeArg5 { get; set; }
        }

        private class OptionsWithCollectionPositionalWhichIsNotLast
        {
            [Argument(0)]
            public string SomeArg0 { get; set; }

            [Argument(1)]
            public string[] SomeArg1 { get; set; }

            [Argument(2)]
            public string SomeArg5 { get; set; }
        }

        private class OptionsWithMultiplePositionalCollections
        {
            [Argument(0)]
            public string[] SomeArg0 { get; set; }

            [Argument(1)]
            public string[] SomeArg1 { get; set; }
        }
    }
}