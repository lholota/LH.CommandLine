using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options.Factoring
{
    internal class CtorOptionsFactoringStrategy<TOptions> : IOptionsFactory<TOptions>
    {
        private readonly OptionsMetadata _optionsMetadata;

        public CtorOptionsFactoringStrategy(OptionsMetadata optionsMetadata)
        {
            _optionsMetadata = optionsMetadata;
        }

        public bool CanCreateOptions()
        {
            return TryFindMatchingConstructor(out _);
        }

        public TOptions CreateOptions(IReadOnlyCollection<PropertyValue> values)
        {
            if (!TryFindMatchingConstructor(out ConstructorInfo ctorInfo))
            {
                throw new InvalidOperationException($"Cannot create options of type {_optionsMetadata.OptionsType} when it has no matching constructor.");
            }

            var ctorParameters = ctorInfo.GetParameters();
            var parameterValues = new object[values.Count];

            for (var i = 0; i < ctorParameters.Length; i++)
            {
                var propertyValue = values.Single(x => ParamMatchesProperty(ctorParameters[i].Name, x.PropertyMetadata.Name));
                parameterValues[i] = propertyValue.Value;
            }

            return (TOptions)ctorInfo.Invoke(parameterValues);
        }

        private bool TryFindMatchingConstructor(out ConstructorInfo ctorInfo)
        {
            var constructors = _optionsMetadata.OptionsType.GetConstructors();

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

            if (parameters.Length != _optionsMetadata.Properties.Count)
            {
                return false;
            }

            foreach (var parameterInfo in parameters)
            {
                if (!_optionsMetadata.Properties.Any(prop => ParamMatchesProperty(parameterInfo.Name, prop.Name)))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ParamMatchesProperty(string parameterName, string propertyName)
        {
            return string.Equals(propertyName, parameterName, StringComparison.OrdinalIgnoreCase);
        }
    }
}