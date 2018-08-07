using System;
using LH.CommandLine.Options;

namespace LH.CommandLine
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SwitchAttribute : NamedAttribute, ISwitch
    {
        public SwitchAttribute(char shortName) 
            : base(shortName)
        {
            SetDefaults();
        }

        public SwitchAttribute(string longName) 
            : base(longName)
        {
            SetDefaults();
        }

        public SwitchAttribute(char shortName, string longName) 
            : base(shortName, longName)
        {
            SetDefaults();
        }

        public object Value { get; set; }

        string[] ISwitch.Aliases
        {
            get => Aliases;
        }

        private void SetDefaults()
        {
            Value = true;
        }
    }
}