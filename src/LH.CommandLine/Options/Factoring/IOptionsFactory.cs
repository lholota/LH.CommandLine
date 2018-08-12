using System.Collections.Generic;

namespace LH.CommandLine.Options.Factoring
{
    internal interface IOptionsFactory<out TOptions>
    {
        bool CanCreateOptions();

        TOptions CreateOptions(IReadOnlyCollection<PropertyValue> values);
    }
}