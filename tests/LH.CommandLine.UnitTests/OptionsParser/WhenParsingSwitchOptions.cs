using LH.CommandLine.Options;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingSwitchOptions
    {
        [Fact]
        public void ShouldSetSwitchWhenSpecified()
        {
            var parser = new OptionsParser<SwitchOptions>();
            var options = parser.Parse(new[] { "--set" });

            Assert.True(options.IsSet);
        }

        [Fact]
        public void ShouldHaveDefaultValueWhenSwitchNotSpecified()
        {
            var parser = new OptionsParser<SwitchOptions>();
            var options = parser.Parse(new string[0]);

            Assert.False(options.IsSet);
        }

        [Fact]
        public void ShouldSetValueWhenSpecified()
        {
            var parser = new OptionsParser<EnumSwitchOptions>();
            var options = parser.Parse(new[] { "--set-enum" });

            Assert.Equal(DummyEnum.One, options.SwitchValue);
        }

        [Fact]
        public void ShouldSupportMultipleSwitchesOnSameProperty()
        {
            var parser = new OptionsParser<MultipleSwitchOptions>();

            var options1 = parser.Parse(new[] {"--one"});
            Assert.Equal(DummyEnum.One, options1.SwitchValue);

            var options2 = parser.Parse(new[] { "--two" });
            Assert.Equal(DummyEnum.Two, options2.SwitchValue);
        }

        private class SwitchOptions
        {
            [Switch("set")]
            public bool IsSet { get; set; }
        }

        private class EnumSwitchOptions
        {
            [Switch("set-enum", Value = DummyEnum.One)]
            public DummyEnum SwitchValue { get; set; }
        }

        private class MultipleSwitchOptions
        {
            [Switch("one", Value = DummyEnum.One)]
            [Switch("two", Value = DummyEnum.Two)]
            public DummyEnum SwitchValue { get; set; }
        }

        private enum DummyEnum
        {
            Zero = 0,
            One = 1,
            Two = 2
        }
    }
}