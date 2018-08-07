using System;
using System.Collections.Generic;

namespace LH.CommandLine.Options.Builders
{
    internal class OptionsBuilderFactory<TOptions>
    {
        public IOptionBuilder<TOptions> CreateBuilder(IEnumerable<OptionProperty> optionValues)
        {
            if (PropertySetterOptionsBuilder<TOptions>.CanBuildOptions(optionValues))
            {
                return new PropertySetterOptionsBuilder<TOptions>();
            }

            throw new Exception($"The type {typeof(TOptions)} doesn't have public properties with a setter for the options or a ctor with parameters for the options.");
        }
    }
}