using LH.CommandLine.Options;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenValidatingOptions
    {
        [Fact]
        public void ReturnOptionsWhenValuesAreValid()
        {
            var parser = new OptionsParser<ValidatedOptions>();
            var options = parser.Parse(new[] { "--email", "some-value" });

            Assert.NotNull(options);
        }

        public class ValidatedOptions
        {
            [Required]
            [Option("email")]
            public string Email { get; set; }    
        }
    }
}