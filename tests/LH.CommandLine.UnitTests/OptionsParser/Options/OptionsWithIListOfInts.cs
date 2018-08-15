using System.Collections.Generic;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithIListOfInts
    {
        [Option("numbers")]
        public IList<int> Numbers { get; set; }
    }
}