using LH.CommandLine.Options2.Metadata;

namespace LH.CommandLine.Options2.Validation
{
    internal class OptionsMetadataValidator
    {
        private readonly OptionsMetadata _optionsMetadata;

        public OptionsMetadataValidator(OptionsMetadata optionsMetadata)
        {
            _optionsMetadata = optionsMetadata;
        }

        public ValidationResult Validate()
        {
            var result = new ValidationResult();

            foreach (var property in _optionsMetadata.Properties)
            {
                var propertyValidator = new OptionPropertyMetadataValidator(property);
                var propertyResult = propertyValidator.ValidatePropertyMetadata();

                result.Merge(propertyResult);
            }

            return result;
        }
    }
}