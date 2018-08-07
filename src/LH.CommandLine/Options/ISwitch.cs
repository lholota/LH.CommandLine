namespace LH.CommandLine.Options
{
    internal interface ISwitch
    {
        string[] Aliases { get; }

        object Value { get; }
    }
}