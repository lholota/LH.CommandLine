namespace LH.CommandLine
{
    public interface ICommand<in TOptions>
    {
        void Execute(TOptions options);
    }
}
