namespace LH.CommandLine
{
    public interface IExecutable
    {
        void Execute(ArgsReader reader);
    }
}