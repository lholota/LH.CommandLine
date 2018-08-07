using System;

namespace LH.CommandLine
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionAttribute : Attribute
    {
        public OptionAttribute(char shortName)
             : this(shortName, null)
        {
            Aliases = new[] { $"--{shortName}" };
        }

        public OptionAttribute(string longName)
        {
            Aliases = new[] {$"--{longName}"};
        }

        public OptionAttribute(char shortName, string longName)
        {
            Aliases = new[]
            {
                $"-{shortName}",
                $"--{longName}"
            };
        }

        internal string[] Aliases { get; }

        public object DefaultValue { get; set; }
    }
}