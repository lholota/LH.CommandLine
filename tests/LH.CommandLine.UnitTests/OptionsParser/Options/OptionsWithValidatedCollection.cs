using System.ComponentModel.DataAnnotations;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithValidatedCollection
    {
        [Required]
        [Range(2, 5)]
        [Option("strings")]
        public string[] Strings { get; set; }
    }
}