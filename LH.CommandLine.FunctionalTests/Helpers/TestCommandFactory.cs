using System;

namespace LH.CommandLine.FunctionalTests.Helpers
{
    public class TestCommandFactory : ICommandFactory
    {
        public object CreatedCommand { get; private set; }

        public TCommand CreateCommand<TCommand>()
        {
            throw new NotImplementedException();
        }
    }
}
