using LH.CommandLine.Options2.Values;

namespace LH.CommandLine.Options2
{
    internal class SingleValueOptionProperty : OptionProperty
    {
        private object _value;

        public SingleValueOptionProperty(OptionPropertyMetadata propertyMetadata, ValueParserSelector parserSelector) 
            : base(propertyMetadata, parserSelector)
        {
        }

        public override object GetValue()
        {
            return _value;
        }

        protected override void UpdateValue(object value)
        {
            _value = value;
        }
    }
}