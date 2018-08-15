using System;

namespace LH.CommandLine.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] GetArraySubset<T>(this T[] array, int startIndex, int length)
        {
            var subsetArray = new T[length];
            
            Array.Copy(array, startIndex, subsetArray, 0, length);

            return subsetArray;
        }
    }
}