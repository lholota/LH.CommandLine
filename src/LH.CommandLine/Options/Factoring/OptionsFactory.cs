using System.Collections.Generic;
using System.Linq;
using LH.CommandLine.Options.Metadata;

namespace LH.CommandLine.Options.Factoring
{
    internal class OptionsFactory<TOptions> : IOptionsFactory<TOptions>
    {
        private readonly IReadOnlyCollection<IOptionsFactory<TOptions>> _strategies;

        public OptionsFactory(OptionsMetadata optionsMetadata)
        {
            _strategies = new IOptionsFactory<TOptions>[]
            {
                new SetterOptionsFactoringStrategy<TOptions>(optionsMetadata),
                new CtorOptionsFactoringStrategy<TOptions>(optionsMetadata)
            };
        }

        public bool CanCreateOptions()
        {
            return FindStrategy() != null;
        }

        public TOptions CreateOptions(IReadOnlyCollection<PropertyValue> values)
        {
            var strategy = FindStrategy();

            return strategy.CreateOptions(values);
        }

        private IOptionsFactory<TOptions> FindStrategy()
        {
            return _strategies.FirstOrDefault(x => x.CanCreateOptions());
        }
    }
}