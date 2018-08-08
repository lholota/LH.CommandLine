namespace LH.CommandLine.Options.Values
{
    public class Int16Parser : IValueParser
    {
        public object Parse(string rawValue)
        {
            return short.Parse(rawValue);
        }
    }
}
