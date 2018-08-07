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

        public OptionsParser()
        {
            _builderFactory = new OptionsBuilderFactory<TOptions>();
            _optionPropertyCollection = new OptionPropertyCollection(typeof(TOptions));
            _optionsValidator = new OptionsValidator();
        }

        public TOptions Parse(string[] args)
        {
            // TODO: Catch the exceptions with individual errors and throw a combined exception with all errors

            var optionBuilder = _builderFactory.CreateBuilder(_optionPropertyCollection);

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
                    ParseAndSetValue(optionBuilder, optionProperty, args[i]);
                    continue;
                }

                if (i + 1 < args.Length)
                {
                    if (_optionPropertyCollection.TryGetPropertyByName(args[i], out optionProperty))
                    {
                        ParseAndSetValue(optionBuilder, optionProperty, args[i+1]);

                        i++;
                        continue;
                    }
                }

                throw new Exception("The value .... is not a valid options or argument");
            }

            var options = optionBuilder.Build();

            _optionsValidator.ValidateOptions(options);

            return options;
        }

        private void ParseAndSetValue(IOptionBuilder<TOptions> builder, OptionProperty property, string rawValue)
        {
            var parser = ValueParsers.GetValueParser(property.Type);
            var parsedValue = parser.Parse(rawValue);

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
    }
}