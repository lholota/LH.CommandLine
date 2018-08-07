using Xunit;

namespace LH.CommandLine.FunctionalTests.OptionsParser
{
    public class WhenUsingDefaultValues
    {
        private const string DefaultValue = "Default";

        [Fact]
        public void ShouldReturnOptionValue_WhenOptionSpecified()
        {
            var parser = new OptionsParser<OptionsWithDefaults>();
            var options = parser.Parse(new[] { "--email", "some-value" });

            Assert.Equal("some-value", options.Email);
        }

        [Fact]
        public void ShouldReturnDefaultValue_WhenOptionNotSpecified()
        {
            var parser = new OptionsParser<OptionsWithDefaults>();
            var options = parser.Parse(new string[0]);

            Assert.Equal(DefaultValue, options.Email);
        }

        public class OptionsWithDefaults
        {
            [Option("email", DefaultValue = DefaultValue)]
            public string Email { get; set; }
        }
    }
}