using System;
using System.Collections.Generic;
using System.Reflection;
using LH.CommandLine.Internal;

namespace LH.CommandLine
{
    public class CommandLineApplication
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IOutputWriter _outputWriter;
        private readonly IDictionary<string, IExecutable> _commands;

        private IExecutable _defaultCommandDefinition;

        public CommandLineApplication(
            ICommandFactory commandFactory,
            IOutputWriter outputWriter)
        {
            _commandFactory = commandFactory;
            _outputWriter = outputWriter;

            _commands = new Dictionary<string, IExecutable>();
        }

        public void AddCommand<TCommand, TOptions>(string name)
            where TCommand : ICommand<TOptions>
        {
            _commands.Add(name, new OptionsCommandDefinition<TCommand, TOptions>(_commandFactory));
        }

        public void AddCommand<TCommand>(string name)
            where TCommand : ICommand
        {
            _commands.Add(name, new CommandDefinition<TCommand>(_commandFactory));
        }

        public void SetDefaultCommand<TCommand, TOptions>()
            where TCommand : ICommand<TOptions>
        {
            _defaultCommandDefinition = new OptionsCommandDefinition<TCommand, TOptions>(_commandFactory);
        }

        public void SetDefaultCommand<TCommand>()
            where TCommand : ICommand
        {
            _defaultCommandDefinition = new CommandDefinition<TCommand>(_commandFactory);
        }

        public void AddCommandGroup(string groupName, Action<ICommandGroupBuilder> configureGroupAction)
        {
            var commandGroupDefinition = new CommandGroupDefinition(_commandFactory);
            configureGroupAction.Invoke(commandGroupDefinition);

            _commands.Add(groupName, commandGroupDefinition);
        }

        public void Run(string[] args)
        {
            if (args.Length == 1 && args[0] == "version")
            {
                _outputWriter.WriteLine(Assembly.GetCallingAssembly().GetName().Version.ToString());
                return;
            }

            if (args.Length == 0)
            {
                _defaultCommandDefinition.Execute(args, 0);
                return;
            }

            var commands = _commands[args[0]];

            commands.Execute(args, 1);
        }
    }
}