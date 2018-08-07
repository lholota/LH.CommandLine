using System;

namespace LH.CommandLine
{
    public interface ICommandGroupBuilder
    {
        void AddCommand<TCommand, TOptions>(string cmd) where TCommand : ICommand<TOptions>;

        void AddCommand<TCommand>(string cmd) where TCommand : ICommand;

        void AddCommandGroup(string name, Action<ICommandGroupBuilder> action);
    }
}
