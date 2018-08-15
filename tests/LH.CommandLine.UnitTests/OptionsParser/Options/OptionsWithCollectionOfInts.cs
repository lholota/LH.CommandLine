using System.Collections.ObjectModel;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithCollectionOfInts
    {
        [Option("numbers")]
        public Collection<int> Numbers { get; set; }
    }
}
