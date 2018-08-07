using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace LH.CommandLine.FunctionalTests.OptionsParser
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

        [Fact]
        public void FailWhenOptionsAreInvalid()
        {
            var parser = new OptionsParser<ValidatedOptions>();

            Assert.Throws<Exception>(
                () => parser.Parse(new[] { "--email" }));
        }

        public class ValidatedOptions
        {
            [Required]
            [Option("email")]
            public string Email { get; set; }    
        }
    }
}