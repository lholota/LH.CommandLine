using System;
using System.Collections.Generic;

namespace LH.CommandLine.Options.Builders
{
    internal class PropertySetterOptionsBuilder<TOptions> : IOptionBuilder<TOptions>
    {
        public static bool CanBuildOptions(IEnumerable<OptionProperty> optionProperties)
        {
            if (!HasParameterlessConstructor(typeof(TOptions)))
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

        private readonly TOptions _instance;

        public PropertySetterOptionsBuilder()
        {
            _instance = Activator.CreateInstance<TOptions>();
        }

        public void SetValue(OptionProperty property, object value)
        {
            property.PropertyInfo.SetValue(_instance, value);
        }

        public TOptions Build()
        {
            return _instance;
        }

        private static bool HasParameterlessConstructor(Type type)
        {
            return type.GetConstructor(new Type[0]) != null;
        }
    }
}