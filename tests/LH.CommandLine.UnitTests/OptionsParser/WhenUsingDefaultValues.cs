using System;
using LH.CommandLine.Options;
using System.ComponentModel;
using LH.CommandLine.Options.Values;
using LH.CommandLine.UnitTests.OptionsParser.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
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
        [DefaultValue(32)]
        public void ShouldReturnDefaultValue_WhenOptionNotSpecified()
        {
            var parser = new OptionsParser<OptionsWithDefaults>();
            var options = parser.Parse(new string[0]);

            Assert.Equal(DefaultValue, options.Email);
        }


        [Fact]
        public void ShouldReturnDefaultValue_WhenDefaultValueIsOfDerivedType()
        {
            var parser = new OptionsParser<OptionsWithDerivedDefaultValue>();
            var options = parser.Parse(new string[0]);

            Assert.Equal(32, options.PropertyA);
        }

        [Fact]
        [DefaultValue(32)]
        public void ShouldReturnDefaultValue_WhenCollectionOptionNotSpecified()
        {
            var parser = new OptionsParser<OptionsWithCollectionWithDefaultValue>();
            var options = parser.Parse(new string[0]);

            Assert.Equal(new[] { "Default" }, options.Strings);
        }

        private class OptionsWithDerivedDefaultValue
        {
            [DefaultValue(32)] // Int32
            [Option("some-option")]
            [ValueParser(typeof(DummyParser))]
            public IComparable PropertyA { get; set; }

            private class DummyParser : ValueParserBase<IComparable>
            {
                public override IComparable Parse(string rawValue)
                {
                    throw new NotImplementedException();
                }
            }
        }

        public class OptionsWithDefaults
        {
            [Option("email")]
            [DefaultValue(DefaultValue)]
            public string Email { get; set; }
        }
    }
}