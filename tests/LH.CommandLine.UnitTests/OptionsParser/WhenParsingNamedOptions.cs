using System.Linq;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingNamedOptions
    {
        [Fact]
        public void ShouldThrowWhenOptionSpecifiedWithoutValue()
        {
            var parser = new OptionsParser<OptionsWithIntAndString>();

            var exception = Assert.Throws<InvalidOptionsException>(
                () => parser.Parse(new[] { "--int", "--string", "AAAA" }));

            Assert.Contains(exception.Errors, error => error.Contains("without a value"));
        }

        private class OptionsWithIntAndString
        {
            [Option("int")]
            public int IntValue { get; set; }

            [Option("string")]
            public string StringValue { get; set; }
        }
    }
}