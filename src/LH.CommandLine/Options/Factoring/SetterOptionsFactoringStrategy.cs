using System;
using System.Collections.Generic;
using LH.CommandLine.Extensions;
using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options.Factoring
{
    internal class SetterOptionsFactoringStrategy<TOptions> : IOptionsFactory<TOptions>
    {
        private readonly OptionsMetadata _optionMetadata;

        public SetterOptionsFactoringStrategy(OptionsMetadata optionMetadata)
        {
            _optionMetadata = optionMetadata;
        }

        public bool CanCreateOptions()
        {
            if (!_optionMetadata.OptionsType.HasParameterlessConstructor())
            {
                return false;
            }

            foreach (var property in _optionMetadata.Properties)
            {
                var setter = property.PropertyInfo.GetSetMethod();

                if (setter == null || !setter.IsPublic)
                {
                    return false;
                }
            }

            return true;
        }

        public TOptions CreateOptions(IReadOnlyCollection<PropertyValue> values)
        {
            var options = Activator.CreateInstance<TOptions>();

            foreach (var propertyValue in values)
            {
                propertyValue.PropertyMetadata.PropertyInfo.SetValue(options, propertyValue.Value);
            }

            return options;
        }
    }
}