using System;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
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

            var parser = new OptionsParser<ByteArrayOptions>();
            var options = parser.Parse(new[] { "--bytes", base64String });

            Assert.Equal(randomBytes, options.Bytes);
        }

        [Fact]
        public void ShouldThrowWhenValueInvalid()
        {
            var parser = new OptionsParser<ByteArrayOptions>();
            Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] {"--bytes", "something%definitely$not;valid"}));
        }

        private class ByteArrayOptions
        {
            [Option("bytes")]
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