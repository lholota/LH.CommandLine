using System;
using System.Collections.Generic;

namespace LH.CommandLine.Options.Factoring
{
    internal class OptionsFactory<TOptions>
    {
        private readonly IOptionsFactoringStrategy<TOptions> _propertySettingFactoringStrategy;

        public OptionsFactory()
        {
            _propertySettingFactoringStrategy = new PropertySettingOptionsFactoringStrategy<TOptions>();
        }

        public TOptions CreateOptions(IReadOnlyList<OptionProperty> optionValues)
        {
            if (_propertySettingFactoringStrategy.CanCreateOptions(optionValues))
            {
                return _propertySettingFactoringStrategy.CreateOptions(optionValues);
            }

            throw new Exception($"The type {typeof(TOptions)} doesn't have public properties with a setter for the options or a ctor with parameters for the options.");
        }
    }
}