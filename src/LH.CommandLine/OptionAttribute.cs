using System;

namespace LH.CommandLine
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionAttribute : NamedAttribute
    {
        public OptionAttribute(char shortName) 
            : base(shortName)
        {
        }

        public OptionAttribute(string longName) 
            : base(longName)
        {
        }

        public OptionAttribute(char shortName, string longName) 
            : base(shortName, longName)
        {
        }
    }
}