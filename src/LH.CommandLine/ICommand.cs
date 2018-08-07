namespace LH.CommandLine
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<in TOptions>
    {
        void Execute(TOptions options);
    }
}
