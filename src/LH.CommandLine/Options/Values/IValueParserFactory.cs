namespace LH.CommandLine.Options.Values
{
    public partial interface IValueParserFactory
    {
        T CreateParser<T>();
    }
}