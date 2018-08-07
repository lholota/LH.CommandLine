using System;
using LH.CommandLine.Options.Factoring;

namespace LH.CommandLine
{
    public class OptionParser<TOptions>
    {
        public TOptions Parse(string[] args)
        {
            // TODO: Catch the exceptions with individual errors and throw a combined exception with all errors

            var optionsBuilder = new OptionsBuilder<TOptions>();

            for (var i = 0; i < args.Length; i++)
            {
                if (optionsBuilder.TrySetSwitch(args[i]))
                {
                    continue;
                }

                if (optionsBuilder.TrySetPositionalArgument(i, args[i]))
                {
                    continue;
                }

                if (i + 1 < args.Length)
                {
                    if (optionsBuilder.TrySetOption(args[i], args[i + 1]))
                    {
                        i++;
                        continue;
                    }
                }

                throw new Exception("The value .... is not a valid options or argument");
            }

            return optionsBuilder.Build();
        }
    }
}