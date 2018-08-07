using System;

namespace LH.CommandLine.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasParameterlessConstructor(this Type type)
        {
            return type.GetConstructor(new Type[0]) != null;
        }
    }
}