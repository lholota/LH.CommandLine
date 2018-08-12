using System;

namespace LH.CommandLine.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsSubclassOfGeneric(this Type checkedType, Type genericBaseType)
        {
            while (checkedType != null && checkedType != typeof(object))
            {
                var current = checkedType.IsGenericType 
                    ? checkedType.GetGenericTypeDefinition() 
                    : checkedType;

                if (current == genericBaseType)
                {
                    return true;
                }

                checkedType = checkedType.BaseType;
            }

            return false;
        }

        public static bool HasParameterlessConstructor(this Type type)
        {
            return type.GetConstructor(new Type[0]) != null;
        }
    }
}