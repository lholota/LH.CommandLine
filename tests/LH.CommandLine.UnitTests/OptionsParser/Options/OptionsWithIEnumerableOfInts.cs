using System.Collections.Generic;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithIEnumerableOfInts
    {
        [Option("numbers")]
        public IEnumerable<int> Numbers { get; set; }
    }
}