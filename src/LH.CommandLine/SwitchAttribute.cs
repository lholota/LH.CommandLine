using System;

namespace LH.CommandLine
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SwitchAttribute : NamedAttribute
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

        private void SetDefaults()
        {
            Value = true;
        }
    }
}