using System;
using LH.CommandLine.Options;
using LH.CommandLine.Options.Values;
using LH.CommandLine.UnitTests.OptionsParser.Options;
using Moq;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenUsingCustomValueParserFactory
    {
        [Fact]
        public void ThrowWhenFactoryFailsToCreateParser()
        {
            var exception = new Exception();

            var factoryMock = new Mock<IValueParserFactory>();
            factoryMock
                .Setup(x => x.CanCreateParser(It.IsAny<Type>()))
                .Returns(true);

            factoryMock
                .Setup(x => x.CreateParser(typeof(OptionsWithCustomParser.CustomParser)))
                .Throws(exception);

            var parser = new OptionsParser<OptionsWithCustomParser>(factoryMock.Object);
            var actualException = Assert.Throws<Exception>(() => parser.Parse(new[] {"--string-option", "some-value"}));

            Assert.Equal(exception, actualException);
        }

        [Fact]
        public void ShouldUseValueParserCreatedByFactory()
        {
            var factoryMock = new Mock<IValueParserFactory>();
            factoryMock
                .Setup(x => x.CanCreateParser(It.IsAny<Type>()))
                .Returns(true);

            factoryMock
                .Setup(x => x.CreateParser(typeof(OptionsWithCustomParser.CustomParser)))
                .Returns(new OptionsWithCustomParser.CustomParser("OtherCustomValue"));

            var parser = new OptionsParser<OptionsWithCustomParser>(factoryMock.Object);
            var options = parser.Parse(new[] { "--string-option", "some-value" });

            Assert.Equal("OtherCustomValue", options.StringOption);
        }
    }
}