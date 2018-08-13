using System;
using System.Collections.Generic;
using LH.CommandLine.Extensions;
using LH.CommandLine.Options.Reflection;

namespace LH.CommandLine.Options.Factoring
{
    internal class SetterOptionsFactoringStrategy<TOptions> : IOptionsFactory<TOptions>
    {
        private readonly OptionsTypeDescriptor _typeDescriptor;

        public SetterOptionsFactoringStrategy(OptionsTypeDescriptor typeDescriptor)
        {
            _typeDescriptor = typeDescriptor;
        }

        public bool CanCreateOptions()
        {
            if (!_typeDescriptor.OptionsType.HasParameterlessConstructor())
            {
                return false;
            }

            foreach (var property in _typeDescriptor.Properties)
            {
                var setter = property.GetSetMethod();

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
                propertyValue.PropertyInfo.SetValue(options, propertyValue.Value);
            }

            return options;
        }
    }
}