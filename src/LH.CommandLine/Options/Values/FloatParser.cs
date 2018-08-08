namespace LH.CommandLine.Options.Values
{
    public class FloatParser : IValueParser
    {
        public object Parse(string rawValue)
        {
            return float.Parse(rawValue);
        }
    }
}