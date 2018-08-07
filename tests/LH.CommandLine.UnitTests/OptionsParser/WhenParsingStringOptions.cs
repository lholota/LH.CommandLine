using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingStringOptions
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
    }

    public class StringOptions
    {
        [Option('n', "name")]
        public string StringOption { get; set; }
    }
}