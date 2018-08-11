using System;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
using LH.CommandLine.Options.BuiltinParsers;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingByteArrayValues
    {
        [Fact]
        public void ShouldParseBase64EncodedValue()
        {
            var randomBytes = GetRandomBytes();
            var base64String = Convert.ToBase64String(randomBytes);

            var parser = new OptionsParser<DefaultByteArrayOptions>();
            var options = parser.Parse(new[] { "--bytes", base64String });

            Assert.Equal(randomBytes, options.Bytes);
        }

        [Fact]
        public void ShouldParseHexValue()
        {
            const string hexValue = "48656c6c6f20776f726c64";
            var expectedBytes = new byte[] { 72, 101, 108, 108, 111, 32, 119, 111, 114, 108, 100 };

            var parser = new OptionsParser<HexByteArrayOptions>();
            var options = parser.Parse(new[] { "--bytes", hexValue });

            Assert.Equal(expectedBytes, options.Bytes);
        }

        [Fact]
        public void ShouldParseHexValueStartingWithZeroX()
        {
            const string hexValue = "0x48656c6c6f20776f726c64";
            var expectedBytes = new byte[] { 72, 101, 108, 108, 111, 32, 119, 111, 114, 108, 100 };

            var parser = new OptionsParser<HexByteArrayOptions>();
            var options = parser.Parse(new[] { "--bytes", hexValue });

            Assert.Equal(expectedBytes, options.Bytes);
        }

        [Fact]
        public void ShouldThrowWhenValueInvalid()
        {
            var parser = new OptionsParser<HexByteArrayOptions>();
            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] {"--bytes", "something%definitely$not;valid"}));
        }

        [Fact]
        public void ShouldThrowWhenHexValueInvalid()
        {
            var parser = new OptionsParser<HexByteArrayOptions>();
            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--bytes", "486" }));
        }

        private class DefaultByteArrayOptions
        {
            [Option("bytes")]
            public byte[] Bytes { get; set; }
        }

        private class HexByteArrayOptions
        {
            [Option("bytes")]
            [ValueParser(typeof(HexByteArrayParser))]
            public byte[] Bytes { get; set; }
        }

        private byte[] GetRandomBytes()
        {
            var bytes = new byte[150];
            var random = new Random();

            random.NextBytes(bytes);

            return bytes;
        }
    }
}