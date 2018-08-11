using System;

namespace LH.CommandLine.Options.Values
{
    internal class ActivatorValueParserFactory : IValueParserFactory
    {
        public T CreateParser<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
