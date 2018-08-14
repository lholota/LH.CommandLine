using System;
using System.Linq;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options.Factoring;
using LH.CommandLine.Options.Metadata;
using LH.CommandLine.Options.Validation;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options
{
    public class OptionsParser<TOptions>
        where TOptions: class
    {
        private readonly OptionsMetadata _optionsMetadata;
        private readonly OptionsValidator _optionsValidator;
        private readonly OptionsFactory<TOptions> _optionsFactory;
        private readonly ValueParserSelector _valueParserSelector;
        private readonly OptionsMetadataValidator<TOptions> _optionsMetadataValidator;

        public OptionsParser(IValueParserFactory valueParserFactory)
        {
            _optionsMetadata = new OptionsMetadata(typeof(TOptions));
            _optionsFactory = new OptionsFactory<TOptions>(_optionsMetadata);
            _valueParserSelector = new ValueParserSelector(valueParserFactory);
            _optionsMetadataValidator = new OptionsMetadataValidator<TOptions>(_optionsMetadata, _optionsFactory, _valueParserSelector);
            _optionsValidator = new OptionsValidator();
        }

        public OptionsParser()
            : this(new ActivatorValueParserFactory())
        {
        }

        public TOptions Parse(string[] args)
        {
            _optionsMetadataValidator.Validate();

            var errorsCollection = new OptionsParsingErrors();
            var optionsValues = new OptionsValues(_optionsMetadata);

            for (var i = 0; i < args.Length; i++)
            {
                if (_optionsMetadata.TryGetSwitchValueByName(args[i], out var switchValue))
                {
                    optionsValues.SetValue(switchValue);
                    continue;
                }

                if (_optionsMetadata.TryGetPropertyByIndex(i, out var positionalProperty))
                {
                    if (positionalProperty.IsCollection)
                    {
                        ParseAndSetCollectionValue(errorsCollection, optionsValues, positionalProperty, args, ref i);
                    }
                    else
                    {
                        ParseAndSetValue(errorsCollection, optionsValues, positionalProperty, args[i]);
                    }

                    continue;
                }

                if (i + 1 < args.Length)
                {
                    if (_optionsMetadata.TryGetPropertyByOptionName(args[i], out var namedOptionProperty))
                    {
                        if (namedOptionProperty.IsCollection)
                        {
                            i++; // Skip the option name
                            ParseAndSetCollectionValue(errorsCollection, optionsValues, positionalProperty, args, ref i);
                        }
                        else
                        {
                            ParseAndSetValue(errorsCollection, optionsValues, namedOptionProperty, args[i + 1]);
                            i++;
                        }
                        continue;
                    }
                }

                errorsCollection.AddInvalidOptionError(args[i]);
            }

            var options = _optionsFactory.CreateOptions(optionsValues);

            var validationErrors = _optionsValidator.ValidateOptions(options);
            errorsCollection.AddValidationErrors(validationErrors);

            if (errorsCollection.Any())
            {
                throw new InvalidOptionsException(errorsCollection);
            }

            return options;
        }

        private void ParseAndSetCollectionValue(OptionsParsingErrors errors, OptionsValues values, OptionPropertyMetadata propertyMetadata, string[] args, ref int index)
        {
            var itemsStartIndex = index;

            while (!_optionsMetadata.IsKeyword(args[index]) && index < args.Length)
            {
                index++;
            }

            var length = index - itemsStartIndex;
            var rawValues = new string[length];
            Array.Copy(args, itemsStartIndex, rawValues, 0, length);

            object parsedValue;
            var parser = _valueParserSelector.GetParserForCollectionProperty(propertyMetadata);

            try
            {
                parsedValue = parser.Parse(rawValues, propertyMetadata);
            }
            catch (Exception)
            {
                errors.AddInvalidValueError(propertyMetadata, rawValues);
                return;
            }

            try
            {
                values.SetValue(propertyMetadata, parsedValue);
            }
            catch (DuplicateValueException)
            {
                errors.AddSpecifiedMultipleTimesError(propertyMetadata);
            }
        }

        private void ParseAndSetValue(OptionsParsingErrors errors, OptionsValues values, OptionPropertyMetadata propertyMetadata, string rawValue)
        {
            object parsedValue;

            var parser = _valueParserSelector.GetParserForProperty(propertyMetadata);

            try
            {
                parsedValue = parser.Parse(rawValue, propertyMetadata.Type);
            }
            catch (Exception)
            {
                errors.AddInvalidValueError(propertyMetadata, rawValue);
                return;
            }

            try
            {
                values.SetValue(propertyMetadata, parsedValue);
            }
            catch (DuplicateValueException)
            {
                errors.AddSpecifiedMultipleTimesError(propertyMetadata);
            }
        }
    }
}