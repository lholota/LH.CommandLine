namespace LH.CommandLine.Internal
{
    internal class OptionsCommandDefinition<TCommand, TOptions> : IExecutable
        where TCommand : ICommand<TOptions>
    {
        private readonly ICommandFactory _commandFactory;

        public OptionsCommandDefinition(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        public void Execute(string[] args)
        {
            var command = _commandFactory.CreateCommand<TCommand>();

            command.Execute(default(TOptions));
        }
    }
}