using System.Collections.Generic;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithReadOnlyListOfInts
    {
        [Option("numbers")]
        public IReadOnlyList<int> Numbers { get; set; }
    }
}