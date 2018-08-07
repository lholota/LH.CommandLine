namespace LH.CommandLine.FunctionalTests.Helpers
{
    public class TestCommand : ICommand
    {
        public bool HasBeenExecuted { get; private set; }

        public void Execute()
        {
            HasBeenExecuted = true;
        }
    }
}