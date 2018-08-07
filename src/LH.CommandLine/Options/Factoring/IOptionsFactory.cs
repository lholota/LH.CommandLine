using System.Collections.Generic;

namespace LH.CommandLine.Options
{
    internal interface IOptionsFactoringStrategy<out TOptions>
    {
        bool CanCreateOptions(IEnumerable<OptionProperty> optionProperties);

        TOptions CreateOptions(IEnumerable<OptionProperty> optionProperties);
    }
}