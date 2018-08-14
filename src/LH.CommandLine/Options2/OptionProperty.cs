using System.Linq;
using LH.CommandLine.Options2.Values;

namespace LH.CommandLine.Options2
{
    internal abstract class OptionProperty
    {
        private readonly IValueParser _valueParser;
        private readonly OptionPropertyMetadata _propertyMetadata;

        protected OptionProperty(OptionPropertyMetadata propertyMetadata, ValueParserSelector parserSelector)
        {
            _propertyMetadata = propertyMetadata;
            _valueParser = parserSelector.GetParserForProperty(propertyMetadata);
        }

        public bool TryHandleSwitch(string arg)
        {

        }

        public bool TryHandlePositional(string arg, int index) // --> If collection, set scope
        {

        }

        public bool TryHandleOption(string arg) // If collection, set scope
        {

        }



        public bool ParseArg(string arg, int index, out bool setScope)
        {
            if (_propertyMetadata.Switches.TryGetValue(arg, out var switchValue))
            {
                UpdateValue(switchValue);
                setScope = false;

                return true;
            }

            if (_propertyMetadata.HasPositionalIndex && _propertyMetadata.PositionalIndex == index) // Collection sets scope
            {
                var parsedValue = _valueParser.Parse(arg, _propertyMetadata.ParsedType);

                UpdateValue(parsedValue);
                setScope = false;
                return true;
            }

            if (_propertyMetadata.OptionAliases.Any(x => x == arg))
            {
                setScope = true;
                return true;
            }

            setScope = false;
            return false;
        }

        public void HandleInScopeArg(string rawValue)
        {
            var parsedValue = _valueParser.Parse(rawValue, _propertyMetadata.ParsedType);

            UpdateValue(parsedValue);
        }

        public abstract object GetValue();

        protected abstract void UpdateValue(object value);
    }
}