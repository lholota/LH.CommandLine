using System;

namespace LH.CommandLine.FunctionalTests.Helpers
{
    public class TestCommandFactory : ICommandFactory
    {
        public object CreatedCommand { get; private set; }

        public TCommand CreateCommand<TCommand>()
        {
            if (typeof(TCommand) == typeof(TestCommandWithOptions))
            {
                CreatedCommand = new TestCommandWithOptions();
                return (TCommand) CreatedCommand;
            }

            if (typeof(TCommand) == typeof(TestCommand))
            {
                CreatedCommand = new TestCommand();
                return (TCommand)CreatedCommand;
            }

            throw new NotSupportedException();
        }
    }
}
