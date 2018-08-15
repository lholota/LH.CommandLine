using LH.CommandLine.Exceptions;
using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options
{
    internal class OptionValue : IOptionValue
    {
        private object _value;
        private bool _isValueSet;

        private readonly OptionPropertyMetadata _propertyMetadata;

        public OptionValue(OptionPropertyMetadata propertyMetadata)
        {
            _propertyMetadata = propertyMetadata;
        }

        public void AddValue(object value)
        {
            if (_isValueSet)
            {
                throw new DuplicateValueException(_propertyMetadata.Name);
            }

            _value = value;
            _isValueSet = true;
        }

        public object GetValue()
        {
            if (!_isValueSet)
            {
                return _propertyMetadata.DefaultValue;
            }

            return _value;
        }
    }
}