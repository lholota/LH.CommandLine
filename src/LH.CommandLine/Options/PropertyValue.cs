using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options
{
    internal class PropertyValue
    {
        public PropertyValue(OptionPropertyMetadata propertyMetadataMetadata, object value)
        {
            PropertyMetadata = propertyMetadataMetadata;
            Value = value;
        }

        public OptionPropertyMetadata PropertyMetadata { get; }

        public object Value { get; }

        //public PropertyValue(PropertyInfo propertyInfo, object value)
        //{
        //    PropertyInfo = propertyInfo;
        //    Value = value;
        //}

        //public PropertyInfo PropertyInfo { get; }

        //public object Value { get; }

        //public Type ValueType
        //{
        //    get => Value?.GetType();
        //}

        //public Type PropertyType
        //{
        //    get => PropertyInfo.PropertyType;
        //}

        //public string PropertyName
        //{
        //    get => PropertyInfo.Name;
        //}

        //public bool IsValid()
        //{
        //    if (Value == null)
        //    {
        //        return PropertyType.IsByRef;
        //    }

        //    return PropertyType.IsAssignableFrom(ValueType);
        //}
    }
}