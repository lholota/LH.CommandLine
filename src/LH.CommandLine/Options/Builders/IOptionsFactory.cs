namespace LH.CommandLine.Options.Builders
{
    internal interface IOptionBuilder<out TOptions>
    {
        void SetValue(OptionProperty property, object value);

        TOptions Build();
    }
}