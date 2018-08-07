using System;

namespace LH.CommandLine
{
    public abstract class NamedAttribute : Attribute
    {
        protected NamedAttribute(char shortName)
            : this(shortName, null)
        {
            Aliases = new[] { $"--{shortName}" };
        }

        protected NamedAttribute(string longName)
        {
            Aliases = new[] { $"--{longName}" };
        }

        protected NamedAttribute(char shortName, string longName)
        {
            Aliases = new[]
            {
                $"-{shortName}",
                $"--{longName}"
            };
        }

        internal string[] Aliases { get; }
    }
}