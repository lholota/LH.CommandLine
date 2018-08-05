using System;

namespace LH.CommandLine
{
    public class HelpCommandDefinition : IExecutable
    {
        public void Execute(ArgsReader reader)
        {
            Console.WriteLine("Help");
        }
    }
}