using System.Collections.Generic;
using System.Linq;

namespace LH.CommandLine.Options.Factoring
{
    internal class OptionsFactory<TOptions> : IOptionsFactory<TOptions>
    {
        private readonly IReadOnlyCollection<IOptionsFactory<TOptions>> _strategies;

        public OptionsFactory(OptionsTypeDescriptor typeDescriptor)
        {
            _strategies = new IOptionsFactory<TOptions>[]
            {
                new SetterOptionsFactoringStrategy<TOptions>(typeDescriptor)
            };
        }

        public bool CanCreateOptions()
        {
            return FindStrategy() != null;
        }

        public TOptions CreateOptions(IEnumerable<PropertyValue> values)
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