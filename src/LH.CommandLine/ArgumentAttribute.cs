using System;

namespace LH.CommandLine
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ArgumentAttribute : Attribute
    {
        public ArgumentAttribute(int index)
        {
            Index = index;
        }

        public int Index { get; }
    }
}