namespace LH.CommandLine.Options
{
    internal interface IOptionValue
    {
        void AddValue(object value);

        object GetValue();
    }
}