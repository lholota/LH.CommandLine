namespace LH.CommandLine.Options2.Values
{
    public interface IValueParserFactory
    {
        T CreateParser<T>();
    }
}