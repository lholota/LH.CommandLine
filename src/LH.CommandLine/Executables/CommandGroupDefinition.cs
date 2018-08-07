using System;
using System.Collections.Generic;

namespace LH.CommandLine.Internal
{
    internal class CommandGroupDefinition : ICommandGroupBuilder, IExecutable
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IDictionary<string, IExecutable> _commands;

        public CommandGroupDefinition(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            _commands = new Dictionary<string, IExecutable>();
        }

        public void AddCommand<TCommand, TOptions>(string name) 
            where TCommand : ICommand<TOptions>
        {
            var commandDefinition = new OptionsCommandDefinition<TCommand, TOptions>(_commandFactory);

            _commands.Add(name, commandDefinition);
        }

        public void AddCommand<TCommand>(string name) where TCommand : ICommand
        {
            var commandDefinition = new CommandDefinition<TCommand>(_commandFactory);

            _commands.Add(name, commandDefinition);
        }

        public void AddCommandGroup(string name, Action<ICommandGroupBuilder> action)
        {
            var groupDefinition = new CommandGroupDefinition(_commandFactory);
            action.Invoke(groupDefinition);

            _commands.Add(name, groupDefinition);
        }

        public void Execute(string[] args, int startIndex)
        {
            var command = _commands[args[startIndex]];
            command.Execute(args, startIndex + 1);
        }
    }
}