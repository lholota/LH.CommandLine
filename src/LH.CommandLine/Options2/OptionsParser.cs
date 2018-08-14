using System.Collections.Generic;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options2.Metadata;
using LH.CommandLine.Options2.Validation;

namespace LH.CommandLine.Options2
{
    public class OptionsParser<TOptions>
    {
        private readonly OptionsMetadata _optionsMetadata;
        private readonly OptionsMetadataValidator _optionsMetadataValidator;
        private readonly IReadOnlyList<OptionProperty> _properties;

        public OptionsParser()
        {
            _optionsMetadata = new OptionsMetadata(typeof(TOptions));
            _optionsMetadataValidator = new OptionsMetadataValidator(_optionsMetadata);
        }

        public TOptions Parse(string[] args)
        {
            ValidateMetadata();

            OptionProperty scopeProperty = null;

            for (var i = 0; i < args.Length; i++)
            {
                foreach (var optionProperty in _properties)
                {
                    // None, Handled, HandledSetScope
                    if (optionProperty.ParseArg(args[i], i, out var setScope))
                    {
                        if (setScope)
                        {
                            scopeProperty = optionProperty;
                        }

                        break;
                    }
                }

                if (scopeProperty != null)
                {
                    scopeProperty.HandleInScopeArg(args[i]);
                }
                else
                {
                    // Error -> unknown option
                }
            }
        }

        private void ValidateMetadata()
        {
            var validationResult = _optionsMetadataValidator.Validate();

            if (!validationResult.IsValid)
            {
                throw new InvalidOptionsDefinitionException(typeof(TOptions), validationResult);
            }
        }
    }
}