using System;
using System.Collections.Generic;
using LH.CommandLine.Extensions;

namespace LH.CommandLine.Options
{
    internal class PropertySettingOptionsFactoringStrategy<TOptions> : IOptionsFactoringStrategy<TOptions>
    {
        public bool CanCreateOptions(IEnumerable<OptionProperty> optionProperties)
        {
            var optionsType = typeof(TOptions);

            if (!optionsType.HasParameterlessConstructor())
            {
                return false;
            }

            foreach (var optionProperty in optionProperties)
            {
                var setter = optionProperty.PropertyInfo.GetSetMethod();

                if (setter == null || !setter.IsPublic)
                {
                    return false;
                }
            }

            return true;
        }

        public TOptions CreateOptions(IEnumerable<OptionProperty> optionProperties)
        {
            var instance = Activator.CreateInstance<TOptions>();

            foreach (var optionProperty in optionProperties)
            {
                optionProperty.PropertyInfo.SetValue(instance, optionProperty.Value);
            }

            return instance;
        }
    }
}