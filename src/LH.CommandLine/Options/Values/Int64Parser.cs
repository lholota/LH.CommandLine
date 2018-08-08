namespace LH.CommandLine.Options.Values
{
    public class Int64Parser : IValueParser
    {
        public object Parse(string rawValue)
        {
            return long.Parse(rawValue);
        }
    }
}