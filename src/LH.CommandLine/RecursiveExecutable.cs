using System;

namespace LH.CommandLine
{
    public abstract class RecursiveExecutable : IExecutable
    {
        private readonly ExecutablesCollection _executables;

        protected RecursiveExecutable()
        {
            _executables = new ExecutablesCollection();
            _executables.Add("help", new HelpCommandDefinition());
        }

        public RecursiveExecutable AddCommand<TOptions, TCommand>(string name, Action<RecursiveExecutable> configCall = null)
        {
            var command = new CommandDefinition<TOptions, TCommand>();

            if (configCall != null)
            {
                configCall.Invoke(command);
            }

            _executables.Add(name, command);

            return this;
        }

        public RecursiveExecutable AddAsyncCommand<TOptions, TCommand>(string name, Action<RecursiveExecutable> config = null)
        {
            throw new NotImplementedException();
        }

        public RecursiveExecutable AddCommandGroup(string name, Action<RecursiveExecutable> configCall = null)
        {
            var group = new CommandGroupDefinition();

            if (configCall != null)
            {
                configCall.Invoke(group);
            }

            _executables.Add(name, group);

            return this;
        }

        public void Execute(ArgsReader reader)
        {
            if (_executables.TryFind(reader.Current, out var nestedExecutable))
            {
                reader.MoveNext();

                nestedExecutable.Execute(reader);
            }
            else
            {
                ExecuteSelf(reader);
            }
        }

        protected abstract void ExecuteSelf(ArgsReader reader);
    }
}