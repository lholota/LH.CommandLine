using System;

namespace LH.CommandLine
{
    internal class CommandGroupDefinition : RecursiveExecutable
    {
        protected override void ExecuteSelf(ArgsReader reader)
        {
            Console.WriteLine("Group help");
        }
    }
}