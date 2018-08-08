using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingStringValues
    {
        [Fact]
        public void ShouldParseString()
        {
            var parser = new OptionsParser<StringOptions>();
            var options = parser.Parse(new[] { "--name", "MyName" });

            Assert.Equal("MyName", options.StringOption);
        }
    }

    public class StringOptions
    {
        [Option("name")]
        public string StringOption { get; set; }
    }
}