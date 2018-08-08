using System;
using LH.CommandLine.Options.Builders;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine.Options
{
    public class OptionsParser<TOptions>
    {
        private readonly OptionsValidator _optionsValidator;
        private readonly OptionPropertyCollection _optionPropertyCollection;
        private readonly OptionsBuilderFactory<TOptions> _builderFactory;
        private readonly OptionsDefinitionValidator _optionsDefinitionValidator;

        public OptionsParser()
        {
            _builderFactory = new OptionsBuilderFactory<TOptions>();
            _optionPropertyCollection = new OptionPropertyCollection(typeof(TOptions));
            _optionsDefinitionValidator = new OptionsDefinitionValidator(typeof(TOptions), _optionPropertyCollection);

            _optionsValidator = new OptionsValidator();
        }

        public TOptions Parse(string[] args)
        {
            _optionsDefinitionValidator.Validate();

            var optionBuilder = _builderFactory.CreateBuilder(_optionPropertyCollection);
            var errorsBuilder = new OptionsErrorsBuilder();

            SetDefaultValues(optionBuilder);

            for (var i = 0; i < args.Length; i++)
            {
                OptionProperty optionProperty;

                if (_optionPropertyCollection.TryGetSwitchPropertyByName(args[i], out optionProperty, out var switchValue))
                {
                    optionBuilder.SetValue(optionProperty, switchValue);
                    continue;
                }

                if (_optionPropertyCollection.TryGetPropertyByIndex(i, out optionProperty))
                {
                    ParseAndSetValue(optionBuilder, errorsBuilder, optionProperty, args[i]);
                    continue;
                }

                if (i + 1 < args.Length)
                {
                    if (_optionPropertyCollection.TryGetPropertyByName(args[i], out optionProperty))
                    {
                        ParseAndSetValue(optionBuilder, errorsBuilder, optionProperty, args[i+1]);

                        i++;
                        continue;
                    }
                }

                errorsBuilder.AddInvalidOptionError(args[i]);
            }

            return CreateOptionsOrThrow(optionBuilder, errorsBuilder);
        }

        private void ParseAndSetValue(IOptionBuilder<TOptions> builder, OptionsErrorsBuilder errorsBuilder, OptionProperty property, string rawValue)
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

            builder.SetValue(property, parsedValue);
        }

        private void SetDefaultValues(IOptionBuilder<TOptions> builder)
        {
            foreach (var optionProperty in _optionPropertyCollection)
            {
                if (optionProperty.HasDefaultValue)
                {
                    builder.SetValue(optionProperty, optionProperty.DefaultValue);
                }
            }
        }

        private TOptions CreateOptionsOrThrow(IOptionBuilder<TOptions> optionBuilder, OptionsErrorsBuilder errorsBuilder)
        {
            if (errorsBuilder.HasErrors && !optionBuilder.CanBuild())
            {
                // Validation cannot be performed as the options cannot even be constructed
                throw errorsBuilder.BuildException();
            }

            var options = optionBuilder.Build();

            _optionsValidator.ValidateOptions(errorsBuilder, options);

            if (errorsBuilder.HasErrors)
            {
                throw errorsBuilder.BuildException();
            }

            return options;
        }
    }
}