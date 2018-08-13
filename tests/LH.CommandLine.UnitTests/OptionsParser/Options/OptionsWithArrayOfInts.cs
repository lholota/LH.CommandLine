namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithArrayOfInts
    {
        [Option("numbers")]
        public int[] Numbers { get; set; }

        [Option("string")]
        public string String { get; set; }
    }
}
