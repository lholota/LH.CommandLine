namespace LH.CommandLine.Options.Values
{
    internal class StringParser : IValueParser
    {
        public object Parse(string input)
        {
            return input;
        }
    }
}