using System;

namespace LH.CommandLine
{
    public class CommandDefinition<TOptions, TCommand> : RecursiveExecutable
    {
        protected override void ExecuteSelf(ArgsReader reader)
        {
            Console.WriteLine("Executing command");

            // throw new System.NotImplementedException();
        }
    }
}