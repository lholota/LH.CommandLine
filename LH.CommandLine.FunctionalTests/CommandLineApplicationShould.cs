using LH.CommandLine.FunctionalTests.Helpers;
using Xunit;

namespace LH.CommandLine.FunctionalTests
{
    public class CommandLineApplicationShould
    {
        private readonly CommandLineApplication _application;
        private readonly TestOutputWriter _outputWriter;
        private readonly TestCommandFactory _commandFactory;

        public CommandLineApplicationShould()
        {
            _outputWriter = new TestOutputWriter();
            _commandFactory = new TestCommandFactory();

            _application = new CommandLineApplication(
                _commandFactory,
                _outputWriter);
        }

        [Fact]
        public void OutputBuiltinVersion()
        {
            var args = new[] { "version" };

            _application.Run(args);

            Assert.Contains(_outputWriter.GetOutput(), "1.0.0");
        }

        [Fact]
        public void RegisterAndRunCommand()
        {
            _application.AddCommand<TestCommand, TestOptions>("cmd");

            _application.Run(new[] { "cmd" });

            var command = Assert.IsType<TestCommand>(_commandFactory.CreatedCommand);
            Assert.True(command.HasBeenExecuted);
        }

        [Fact]
        public void RegisterAndDefaultCommand()
        {
            _application.SetDefaultCommand<TestCommand, TestOptions>("cmd");

            _application.Run(new string[0]);

            var command = Assert.IsType<TestCommand>(_commandFactory.CreatedCommand);
            Assert.True(command.HasBeenExecuted);
        }
    }
}