using System;
using LH.CommandLine.Options.Builders;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options
{
    public class OptionsParser<TOptions>
    {
        private readonly OptionsValidator _optionsValidator;
        private readonly OptionPropertyCollection _optionPropertyCollection;
        private readonly IOptionBuilder<TOptions> _optionsBuilder;
        private readonly OptionsDefinitionValidator _optionsDefinitionValidator;

        public OptionsParser()
        {
            _optionPropertyCollection = new OptionPropertyCollection(typeof(TOptions));
            _optionsDefinitionValidator = new OptionsDefinitionValidator(typeof(TOptions), _optionPropertyCollection);

            var builderFactory = new OptionsBuilderFactory<TOptions>();
            _optionsBuilder = builderFactory.CreateBuilder(_optionPropertyCollection);

            _optionsValidator = new OptionsValidator();
        }

        public TOptions Parse(string[] args)
        {
            _optionsDefinitionValidator.Validate();

            var optionValues = new OptionsValues();
            var errorsBuilder = new OptionsErrorsBuilder();

            SetDefaultValues(optionValues);

            for (var i = 0; i < args.Length; i++)
            {
                OptionProperty optionProperty;

                if (_optionPropertyCollection.TryGetSwitchPropertyByName(args[i], out optionProperty, out var switchValue))
                {
                    optionValues.SetValue(optionProperty, switchValue);
                    continue;
                }

                if (_optionPropertyCollection.TryGetPropertyByIndex(i, out optionProperty))
                {
                    ParseAndSetValue(optionValues, errorsBuilder, optionProperty, args[i]);
                    continue;
                }

                if (i + 1 < args.Length)
                {
                    if (_optionPropertyCollection.TryGetPropertyByName(args[i], out optionProperty))
                    {
                        ParseAndSetValue(optionValues, errorsBuilder, optionProperty, args[i+1]);

                        i++;
                        continue;
                    }
                }

                errorsBuilder.AddInvalidOptionError(args[i]);
            }

            return CreateOptionsOrThrow(optionValues, errorsBuilder);
        }

        private void ParseAndSetValue(OptionsValues optionValues, OptionsErrorsBuilder errorsBuilder, OptionProperty property, string rawValue)
        {
            var parser = ValueParsers.GetValueParser(property.Type);
            object parsedValue;

            try
            {
                parsedValue = parser.Parse(rawValue);
            }
            catch (Exception)
            {
                errorsBuilder.AddInvalidValueError(property, rawValue);
                return;
            }

            optionValues.SetValue(property, parsedValue);
        }

        private void SetDefaultValues(OptionsValues optionValues)
        {
            foreach (var optionProperty in _optionPropertyCollection)
            {
                if (optionProperty.HasDefaultValue)
                {
                    optionValues.SetDefaultValue(optionProperty);
                }
            }
        }

        private TOptions CreateOptionsOrThrow(OptionsValues optionValues, OptionsErrorsBuilder errorsBuilder)
        {
            if (errorsBuilder.HasErrors && !_optionsBuilder.CanBuild())
            {
                // Validation cannot be performed as the options cannot even be constructed
                throw errorsBuilder.BuildException();
            }

            var options = _optionsBuilder.Build();

            _optionsValidator.ValidateOptions(errorsBuilder, options);

            if (errorsBuilder.HasErrors)
            {
                throw errorsBuilder.BuildException();
            }

            return options;
        }
    }
}