using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options.Values
{
    internal interface ICollectionValueParser
    {
        object Parse(string[] rawValues, OptionPropertyMetadata propertyMetadata);
    }
}