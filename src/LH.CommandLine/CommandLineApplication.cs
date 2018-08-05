using System;

namespace LH.CommandLine
{
    public class CommandLineApplication : RecursiveExecutable
    {
        private RecursiveExecutable _defaultCommand;

        public CommandLineApplication()
        {
            // TODO: Set defaults
        }

        public void Run(string[] args)
        {
            var reader = new ArgsReader(args);

            Execute(reader);
        }

        public void SetDefaultCommand<TOptions, TCommand>()
        {
            _defaultCommand = new CommandDefinition<TOptions, TCommand>();
        }

        protected override void ExecuteSelf(ArgsReader reader)
        {
            Console.WriteLine("Application help");
        }
    }
}
