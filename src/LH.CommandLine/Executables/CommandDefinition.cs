namespace LH.CommandLine.Executables
{
    public class CommandDefinition<TCommand> : IExecutable
        where TCommand : ICommand
    {
        private readonly ICommandFactory _commandFactory;

        public CommandDefinition(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        public void Execute(string[] args, int startIndex)
        {
            var command = _commandFactory.CreateCommand<TCommand>();
            command.Execute();
        }
    }
}