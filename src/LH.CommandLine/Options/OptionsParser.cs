using System;
using System.Linq;
using System.Reflection;
using LH.CommandLine.Exceptions;
using LH.CommandLine.Options.Factoring;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options
{
    public class OptionsParser<TOptions>
    {
        private readonly OptionsValidator _optionsValidator;
        private readonly OptionsTypeDescriptor _typeDescriptor;
        private readonly OptionsFactory<TOptions> _optionsFactory;
        private readonly OptionsDefinitionValidator _optionsDefinitionValidator;

        public OptionsParser()
        {
            _typeDescriptor = new OptionsTypeDescriptor(typeof(TOptions));
            _optionsFactory = new OptionsFactory<TOptions>(_typeDescriptor);
            _optionsValidator = new OptionsValidator();
            _optionsDefinitionValidator = new OptionsDefinitionValidator(_typeDescriptor);
        }

        public TOptions Parse(string[] args)
        {
            _optionsDefinitionValidator.Validate();

            var errorsCollection = new OptionsParsingErrors();
            var optionsValues = new OptionsValues(_typeDescriptor);

            for (var i = 0; i < args.Length; i++)
            {
                if (_typeDescriptor.TryFindSwitchValue(args[i], out var switchValue))
                {
                    optionsValues.SetValue(switchValue);
                    continue;
                }

                if (_typeDescriptor.TryFindPropertyByPositionalIndex(i, out var positionalProperty))
                {
                    ParseAndSetValue(errorsCollection, optionsValues, positionalProperty, args[i]);
                    continue;
                }

                if (i + 1 < args.Length)
                {
                    if (_typeDescriptor.TryFindPropertyByOptionName(args[i], out var namedOptionProperty))
                    {
                        ParseAndSetValue(errorsCollection, optionsValues, namedOptionProperty, args[i + 1]);

                        i++;
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

        private void ParseAndSetValue(OptionsParsingErrors errors, OptionsValues values, PropertyInfo propertyInfo, string rawValue)
        {
            var parser = ValueParsers.GetValueParser(propertyInfo.PropertyType);
            object parsedValue;

            try
            {
                parsedValue = parser.Parse(rawValue);
            }
            catch (Exception)
            {
                errors.AddInvalidValueError(propertyInfo, rawValue);
                return;
            }

            values.SetValue(propertyInfo, parsedValue);
        }
    }
}