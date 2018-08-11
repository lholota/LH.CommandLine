using System;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options.BuiltinParsers
{
    internal class BuiltinValueParserFactory : IValueParserFactory
    {
        public bool CanCreateParser(Type parserType)
        {
            throw new NotImplementedException();
        }

        public IValueParser CreateParser(Type parserType)
        {
            throw new NotImplementedException();
        }
    }
}
