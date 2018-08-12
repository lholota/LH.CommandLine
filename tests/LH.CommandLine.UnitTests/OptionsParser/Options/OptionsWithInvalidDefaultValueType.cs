using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LH.CommandLine.UnitTests.OptionsParser.Options
{
    public class OptionsWithInvalidDefaultValueType
    {
        [DefaultValue(true)]
        [Option("some-option")]
        public string PropertyA { get; set; }
    }
}
