using System;
using System.Text;

namespace LH.CommandLine.FunctionalTests.Helpers
{
    public class TestOutputWriter : IOutputWriter
    {
        private readonly StringBuilder _outputBuilder;

        public TestOutputWriter()
        {
            _outputBuilder = new StringBuilder();
        }

        public string GetOutput()
        {
            return _outputBuilder.ToString();
        }

        public void WriteLine(string message)
        {
            _outputBuilder.AppendLine(message);
        }
    }
}
