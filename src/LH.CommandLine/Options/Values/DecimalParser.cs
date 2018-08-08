namespace LH.CommandLine.Options.Values
{
    public class DecimalParser : IValueParser
    {
        public object Parse(string rawValue)
        {
            return decimal.Parse(rawValue);
        }
    }
}