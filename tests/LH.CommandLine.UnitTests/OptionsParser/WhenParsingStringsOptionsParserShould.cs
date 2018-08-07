using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingStringsOptionsParserShould
    {
        [Theory]
        [InlineData(new[] { "--name", "MyName" }, "MyName")]
        [InlineData(new[] { "-n", "MyName" }, "MyName")]
        public void ParseStringOptions(string[] args, string expectedValue)
        {
            var parser = new OptionsParser<StringOptions>();
            var options = parser.Parse(args);

            Assert.Equal(expectedValue, options.StringOption);
        }

        [Theory]
        [InlineData(new object[] { new[] { "--name", "aaaa@bbbb" } })]
        public void ThrowWhenStringFailsValidation(string[] args)
        {
            var parser = new OptionsParser<StringOptions>();

            Assert.Throws<Exception>(() => parser.Parse(args));
        }
    }

    public class StringOptions
    {
        [Option('n', "name")]
        public string StringOption { get; set; }
    }

    public class ValidatedStringOptions
    {
        [EmailAddress]
        [Option('n', "name")]
        public string Email { get; set; }
    }
}
