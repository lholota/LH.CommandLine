namespace LH.CommandLine.FunctionalTests.Helpers
{
    public class TestCommand : ICommand<TestOptions>
    {
        public TestCommand()
        {
            HasBeenExecuted = false;
        }

        public bool HasBeenExecuted { get; private set; }

        public void Execute(TestOptions options)
        {
            HasBeenExecuted = true;
        }
    }
}