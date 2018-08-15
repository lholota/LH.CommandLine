using System.Collections.Generic;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithCollectionSwitches
    {
        [Option("numbers")]
        [Switch("one", Value = new[] { 7, 8 })]
        [Switch("two", Value = new[] { 9, 10 })]
        public int[] Numbers { get; set; }
    }
}