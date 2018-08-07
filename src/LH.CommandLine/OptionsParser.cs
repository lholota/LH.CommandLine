using System;
using LH.CommandLine.Options;
using LH.CommandLine.Options.Builders;
using LH.CommandLine.Options.Values;

namespace LH.CommandLine
{
    public class OptionsParser<TOptions>
    {
        private readonly OptionsValidator _optionsValidator;
        private readonly IOptionBuilder<TOptions> _optionBuilder;
        private readonly OptionPropertyCollection _optionPropertyCollection;

        public OptionsParser()
        {
            var builderFactory = new OptionsBuilderFactory<TOptions>();

            _optionPropertyCollection = new OptionPropertyCollection(typeof(TOptions));
            _optionsValidator = new OptionsValidator();
            _optionBuilder = builderFactory.CreateBuilder(_optionPropertyCollection);
        }

        public TOptions Parse(string[] args)
        {
            // TODO: Catch the exceptions with individual errors and throw a combined exception with all errors

            for (var i = 0; i < args.Length; i++)
            {
                OptionProperty optionProperty;

                if (_optionPropertyCollection.TryGetSwitchPropertyByName(args[i], out optionProperty))
                {
                    SetValue(optionProperty, "true"); // TODO: Add special version for switch
                    continue;
                }

                if (_optionPropertyCollection.TryGetPropertyByIndex(i, out optionProperty))
                {
                    SetValue(optionProperty, args[i]);
                    continue;
                }

                if (i + 1 < args.Length)
                {
                    if (_optionPropertyCollection.TryGetPropertyByName(args[i], out optionProperty))
                    {
                        SetValue(optionProperty, args[i+1]);

                        i++;
                        continue;
                    }
                }

                throw new Exception("The value .... is not a valid options or argument");
            }

            var options = _optionBuilder.Build();

            _optionsValidator.ValidateOptions(options);

            return _optionBuilder.Build();
        }

        private void SetValue(OptionProperty property, string rawValue)
        {
            var parser = ValueParsers.GetValueParser(property.Type);
            var parsedValue = parser.Parse(rawValue);

            _optionBuilder.SetValue(property, parsedValue);
        }
    }
}