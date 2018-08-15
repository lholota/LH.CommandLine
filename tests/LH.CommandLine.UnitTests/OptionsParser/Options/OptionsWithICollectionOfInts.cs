using System.Collections.Generic;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithICollectionOfInts
    {
        [Option("numbers")]
        public ICollection<int> Numbers { get; set; }
    }
}