namespace LH.CommandLine.Options.Values
{
    public interface IValueParserFactory
    {
        T CreateParser<T>();
    }
}