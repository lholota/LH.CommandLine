using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingEnums
    {
        [Fact]
        public void ShouldParseStringEnumValue()
        {
            var parser = new OptionsParser<OptionsWithEnum>();
            var options = parser.Parse(new []{ "--enum-value", "One" });

            Assert.Equal(DummyEnum.One, options.EnumValue);
        }

        [Fact]
        public void ShouldParseNumericEnumValue()
        {
            var parser = new OptionsParser<OptionsWithEnum>();
            var options = parser.Parse(new[] { "--enum-value", "2" });

            Assert.Equal(DummyEnum.Two, options.EnumValue);
        }

        [Fact]
        public void ShouldThrowWhenStringValueInvalid()
        {
            var parser = new OptionsParser<OptionsWithEnum>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] {"--enum-value", "SomeInvalidString"}));
        }

        [Fact]
        public void ShouldThrowWhenNumericValueInvalid()
        {
            var parser = new OptionsParser<OptionsWithEnum>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--enum-value", "55" }));
        }

        private class OptionsWithEnum
        {
            [Option("enum-value")]
            public DummyEnum EnumValue { get; set; }
        }

        private enum DummyEnum
        {
            Zero = 0,
            One = 1,
            Two = 2
        }
    }
}