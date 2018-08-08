namespace LH.CommandLine.Options.Values
{
    public class Int32Parser : IValueParser
    {
        public object Parse(string rawValue)
        {
            return int.Parse(rawValue);
        }
    }
}