namespace LH.CommandLine.Executables
{
    internal interface IExecutable
    {
        void Execute(string[] args, int startIndex);
    }
}