namespace LH.CommandLine
{
    public interface ICommandFactory
    {
        TCommand CreateCommand<TCommand>();
    }
}