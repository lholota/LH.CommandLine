using System;

namespace LH.CommandLine.Options.Values
{
    internal class ActivatorValueParserFactory : IValueParserFactory
    {
        public bool CanCreateParser(Type parserType)
        {
            return parserType.GetConstructor(new Type[0]) != null;
        }

        public IValueParser CreateParser(Type parserType)
        {
            return (IValueParser)Activator.CreateInstance(parserType);
        }
    }
}
