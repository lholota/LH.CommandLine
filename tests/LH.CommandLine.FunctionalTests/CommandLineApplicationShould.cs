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

            Assert.Contains("1.0.0", _outputWriter.GetOutput());
        }

        [Fact]
        public void RegisterAndRunCommandWithoutOptions()
        {
            _application.AddCommand<TestCommand>("cmd");

            _application.Run(new[] { "cmd" });

            var command = Assert.IsType<TestCommand>(_commandFactory.CreatedCommand);
            Assert.True(command.HasBeenExecuted);
        }

        [Fact]
        public void RegisterAndRunCommandWithOptions()
        {
            _application.AddCommand<TestCommandWithOptions, TestOptions>("cmd");

            _application.Run(new[] { "--name", "some-value" });

            var command = Assert.IsType<TestCommandWithOptions>(_commandFactory.CreatedCommand);

            Assert.True(command.HasBeenExecuted);
            Assert.Equal("some-value", command.OptionValue);
        }

        [Fact]
        public void RegisterAndRunDefaultCommandWithoutOptions()
        {
            _application.SetDefaultCommand<TestCommand>();

            _application.Run(new string[0]);

            var command = Assert.IsType<TestCommand>(_commandFactory.CreatedCommand);
            Assert.True(command.HasBeenExecuted);
        }

        [Fact]
        public void RegisterAndRunDefaultCommandWithOptionsCommand()
        {
            _application.SetDefaultCommand<TestCommandWithOptions, TestOptions>();

            _application.Run(new[]{ "--name", "some-value" });

            var command = Assert.IsType<TestCommandWithOptions>(_commandFactory.CreatedCommand);

            Assert.True(command.HasBeenExecuted);
            Assert.Equal("some-value", command.OptionValue);
        }

        [Fact]
        public void RegisterAndRunCommandWithoutOptionsInGroup()
        {
            _application.AddCommandGroup("group", cfg =>
            {
                cfg.AddCommand<TestCommand>("cmd");
            });

            _application.Run(new[] { "group", "cmd" });

            var command = Assert.IsType<TestCommand>(_commandFactory.CreatedCommand);
            Assert.True(command.HasBeenExecuted);
        }

        [Fact]
        public void RegisterAndRunCommandWithOptionsInGroup()
        {
            _application.AddCommandGroup("group", cfg =>
            {
                cfg.AddCommand<TestCommandWithOptions, TestOptions>("cmd");
            });

            _application.Run(new[] { "group", "cmd", "--name", "some-value" });

            var command = Assert.IsType<TestCommandWithOptions>(_commandFactory.CreatedCommand);

            Assert.True(command.HasBeenExecuted);
            Assert.Equal("some-value", command.OptionValue);
        }

        [Fact]
        public void RegisterAndRunCommandWithoutOptionsInNestedGroup()
        {
            _application.AddCommandGroup("group", cfg =>
            {
                cfg.AddCommandGroup("nested", nestedCfg =>
                {
                    nestedCfg.AddCommand<TestCommand>("cmd");
                });
            });

            _application.Run(new[] { "group", "nested", "cmd" });

            var command = Assert.IsType<TestCommand>(_commandFactory.CreatedCommand);
            Assert.True(command.HasBeenExecuted);
        }

        [Fact]
        public void RegisterAndRunCommandWithOptionsInNestedGroup()
        {
            _application.AddCommandGroup("group", cfg =>
            {
                cfg.AddCommandGroup("nested", nestedCfg =>
                {
                    nestedCfg.AddCommand<TestCommandWithOptions, TestOptions>("cmd");
                });
            });

            _application.Run(new[] { "group", "nested", "cmd", "--name", "some-value" });

            var command = Assert.IsType<TestCommandWithOptions>(_commandFactory.CreatedCommand);

            Assert.True(command.HasBeenExecuted);
            Assert.Equal("some-value", command.OptionValue);
        }
    }
}