namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithArrayOfIntsAndStringOption
    {
        [Option("numbers")]
        public int[] Numbers { get; set; }

        [Option("string")]
        public string String { get; set; }
    }
}