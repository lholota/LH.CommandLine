using System.Collections.Generic;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithCollectionSwitches
    {
        [Option("numbers")]
        [Switch("one", Value = 1)]
        [Switch("two", Value = 2)]
        public List<int> Numbers { get; set; }
    }
}