namespace LH.CommandLine.FunctionalTests.Helpers
{
    public class TestCommandWithOptions : ICommand<TestOptions>
    {
        public TestCommandWithOptions()
        {
            HasBeenExecuted = false;
        }

        public bool HasBeenExecuted { get; private set; }

        public string OptionValue { get; private set; }

        public void Execute(TestOptions options)
        {
            HasBeenExecuted = true;
            OptionValue = options.Name;
        }
    }
}