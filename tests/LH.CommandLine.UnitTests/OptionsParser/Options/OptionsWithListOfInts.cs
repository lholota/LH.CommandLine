using System.Collections.Generic;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithListOfInts
    {
        [Option("numbers")]
        public List<int> Numbers { get; set; }
    }
}