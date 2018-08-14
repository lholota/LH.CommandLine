using System.Collections.Generic;
using System.Linq;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options2.Metadata;
using LH.CommandLine.Options2.Validation;

namespace LH.CommandLine.Options2
{
    public class OptionsParser<TOptions>
    {
        private readonly OptionsMetadata _optionsMetadata;
        private readonly OptionsMetadataValidator _optionsMetadataValidator;

        public OptionsParser()
        {
            _optionsMetadata = new OptionsMetadata(typeof(TOptions));
            _optionsMetadataValidator = new OptionsMetadataValidator(_optionsMetadata);
        }

        public TOptions Parse(string[] args)
        {
            ValidateMetadata();

            OptionPropertyMetadata scopeProperty = null;

            for (var i = 0; i < args.Length; i++)
            {
                foreach (var property in _optionsMetadata.Properties)
                {
                    if (property.Switches.TryGetValue(args[i], out var switchValue))
                    {
                        // TODO: Set value

                        scopeProperty = null;

                        break;
                    }

                    if (property.HasPositionalIndex && property.PositionalIndex == i)
                    {
                        // TODO: Parse & set value

                        scopeProperty = property.IsCollection 
                            ? property 
                            : null;

                        break;
                    }

                    if (property.OptionAliases.Contains(args[i]))
                    {
                        // TODO: Parse & set value

                        scopeProperty = property.IsCollection
                            ? property
                            : null;

                        break;
                    }

                    if (scopeProperty != null)
                    {
                        // TODO: Add value to scope property
                    }
                    else
                    {
                        // TODO: Throw unknown option
                    }
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