using System.Collections.Generic;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithReadOnlyCollectionOfInts
    {
        [Option("numbers")]
        public IReadOnlyCollection<int> Numbers { get; set; }
    }
}
