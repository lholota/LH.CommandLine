using LH.CommandLine.Options;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LH.CommandLine.Exceptions;
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

        [Fact]
        public void ThrowWhenOptionsAreInvalid()
        {
            var parser = new OptionsParser<ValidatedOptions>();

            Assert.Throws<InvalidOptionsException>(() => parser.Parse(new[] {"--email", ""}));
        }

        [Fact]
        public void ThrowWithCustomMessageWhenOptionsAreInvalid()
        {
            var parser = new OptionsParser<ValidatedOptionsWithCustomMessage>();

            var exception = Assert.Throws<InvalidOptionsException>(() => parser.Parse(new[] { "--email", "" }));

            Assert.Contains(exception.Errors, e => e.Contains("MyMessage"));
        }

        public class ValidatedOptions
        {
            [Required(AllowEmptyStrings = false)]
            [Option("email")]
            public string Email { get; set; }    
        }

        public class ValidatedOptionsWithCustomMessage
        {
            [Required(ErrorMessage = "MyMessage")]
            [Option("email")]
            public string Email { get; set; }
        }
    }
}