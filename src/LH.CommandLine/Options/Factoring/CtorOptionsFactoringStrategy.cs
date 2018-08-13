using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LH.CommandLine.Options.Reflection;

namespace LH.CommandLine.Options.Factoring
{
    internal class CtorOptionsFactoringStrategy<TOptions> : IOptionsFactory<TOptions>
    {
        private readonly OptionsTypeDescriptor _typeDescriptor;

        public CtorOptionsFactoringStrategy(OptionsTypeDescriptor typeDescriptor)
        {
            _typeDescriptor = typeDescriptor;
        }

        public bool CanCreateOptions()
        {
            return TryFindMatchingConstructor(out _);
        }

        public TOptions CreateOptions(IReadOnlyCollection<PropertyValue> values)
        {
            if (!TryFindMatchingConstructor(out ConstructorInfo ctorInfo))
            {
                throw new InvalidOperationException($"Cannot create options of type {_typeDescriptor.OptionsType} when it has no matching constructor.");
            }

            var ctorParameters = ctorInfo.GetParameters();
            var parameterValues = new object[values.Count];

            for (var i = 0; i < ctorParameters.Length; i++)
            {
                var propertyValue = values.Single(x => ParamMatchesProperty(ctorParameters[i], x.PropertyInfo));
                parameterValues[i] = propertyValue.Value;
            }

            return (TOptions)ctorInfo.Invoke(parameterValues);
        }

        private bool TryFindMatchingConstructor(out ConstructorInfo ctorInfo)
        {
            var constructors = _typeDescriptor.OptionsType.GetConstructors();

            foreach (var constructorInfo in constructors)
            {
                if (IsConstructorMatch(constructorInfo))
                {
                    ctorInfo = constructorInfo;
                    return true;
                }
            }

            ctorInfo = null;
            return false;
        }

        private bool IsConstructorMatch(ConstructorInfo ctorInfo)
        {
            var parameters = ctorInfo.GetParameters();

            if (parameters.Length != _typeDescriptor.Properties.Count)
            {
                return false;
            }

            foreach (var parameterInfo in parameters)
            {
                if (!_typeDescriptor.Properties.Any(prop => ParamMatchesProperty(parameterInfo, prop)))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ParamMatchesProperty(ParameterInfo parameter, PropertyInfo property)
        {
            return string.Equals(property.Name, parameter.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}