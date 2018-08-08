using System.Globalization;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingNumberValues
    {
        [Fact]
        public void ShouldParseShortValue()
        {
            var parser = new OptionsParser<ShortOptions>();
            var options = parser.Parse(new[] { "--value", "32" });

            Assert.Equal(32, options.Value);
        }

        [Fact]
        public void ShouldThrowWhenShortValueInvalid()
        {
            var parser = new OptionsParser<ShortOptions>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--value", "32ab" }));
        }

        [Fact]
        public void ShouldParseIntValue()
        {
            var parser = new OptionsParser<IntOptions>();
            var options = parser.Parse(new[] {"--value", "32"});

            Assert.Equal(32, options.Value);
        }

        [Fact]
        public void ShouldThrowWhenIntValueInvalid()
        {
            var parser = new OptionsParser<IntOptions>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--value", "32ab" }));
        }

        [Fact]
        public void ShouldParseLongValue()
        {
            var parser = new OptionsParser<LongOptions>();
            var options = parser.Parse(new[] { "--value", "32" });

            Assert.Equal(32, options.Value);
        }

        [Fact]
        public void ShouldThrowWhenLongValueInvalid()
        {
            var parser = new OptionsParser<LongOptions>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--value", "32ab" }));
        }

        [Fact]
        public void ShouldParseDecimalValue()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            var parser = new OptionsParser<DecimalOptions>();
            var options = parser.Parse(new[] { "--value", "32.598" });

            Assert.Equal(32.598m, options.Value);
        }

        [Fact]
        public void ShouldThrowWhenDecimalValueInvalid()
        {
            var parser = new OptionsParser<LongOptions>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--value", "32ab.45" }));
        }

        [Fact]
        public void ShouldParseFloatValue()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            var parser = new OptionsParser<FloatOptions>();
            var options = parser.Parse(new[] { "--value", "32.598" });

            Assert.Equal(32.598f, options.Value);
        }

        [Fact]
        public void ShouldThrowWhenFloatValueInvalid()
        {
            var parser = new OptionsParser<FloatOptions>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--value", "32ab.45" }));
        }

        [Fact]
        public void ShouldParseDoubleValue()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            var parser = new OptionsParser<DoubleOptions>();
            var options = parser.Parse(new[] { "--value", "32.598" });

            Assert.Equal(32.598d, options.Value);
        }

        [Fact]
        public void ShouldThrowWhenDoubleValueInvalid()
        {
            var parser = new OptionsParser<DoubleOptions>();

            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--value", "32ab.45" }));
        }

        private class ShortOptions
        {
            [Option("value")]
            public short Value { get; set; }
        }

        private class IntOptions
        {
            [Option("value")]
            public int Value { get; set; }
        }

        private class LongOptions
        {
            [Option("value")]
            public long Value { get; set; }
        }

        private class DecimalOptions
        {
            [Option("value")]
            public decimal Value { get; set; }
        }

        private class FloatOptions
        {
            [Option("value")]
            public float Value { get; set; }
        }

        private class DoubleOptions
        {
            [Option("value")]
            public double Value { get; set; }
        }
    }
}