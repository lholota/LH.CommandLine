using LH.CommandLine.Exceptions;
using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenUsingOptionsWithoutDefaultConstructor
    {
        [Fact]
        public void ShouldCreateWhenCtorMatchesProperties()
        {
            var parser = new OptionsParser<OptionsWithAllPropertiesInCtor>();
            var options = parser.Parse(new[] { "--value1", "some", "--value2", "other" });

            Assert.Equal("some", options.Value1);
            Assert.Equal("other", options.Value2);
        }

        [Fact]
        public void ShouldThrowWhenOptionsHaveCtorWithUnknownParameter()
        {
            var parser = new OptionsParser<OptionsWithUnknownCtorParameter>();

            Assert.Throws<InvalidOptionsDefinitionException>(
                () => parser.Parse(new string[0]));
        }

        [Fact]
        public void ShouldThrowWhenOptionsIsMissingOptionsInCtor()
        {
            var parser = new OptionsParser<OptionsWithMissingOptionsInCtor>();

            Assert.Throws<InvalidOptionsDefinitionException>(
                () => parser.Parse(new string[0]));
        }

        private class OptionsWithAllPropertiesInCtor
        {
            [Option("value1")]
            public string Value1 { get; }

            [Option("value2")]
            public string Value2 { get; }

            public OptionsWithAllPropertiesInCtor(string value1, string value2)
            {
                Value1 = value1;
                Value2 = value2;
            }
        }

        private class OptionsWithUnknownCtorParameter
        {
            [Option("value1")]
            public string Value1 { get; }

            public OptionsWithUnknownCtorParameter(string value1, string unkwnownParameter)
            {
                Value1 = value1;
            }
        }

        private class OptionsWithMissingOptionsInCtor
        {
            [Option("value1")]
            public string Value1 { get; }

            [Option("value2")]
            public string Value2 { get; }

            public OptionsWithMissingOptionsInCtor(string value1)
            {
                Value1 = value1;
                Value2 = "Doesn't really matter...";
            }
        }
    }
}