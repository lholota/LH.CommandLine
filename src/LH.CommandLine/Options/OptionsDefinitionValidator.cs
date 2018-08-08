using System.Linq;
using System.Text;
using LH.CommandLine.Exceptions;

namespace LH.CommandLine.Options
{
    internal class OptionsDefinitionValidator
    {
        private readonly OptionPropertyCollection _optionProperties;

        public OptionsDefinitionValidator(OptionPropertyCollection optionProperties)
        {
            _optionProperties = optionProperties;
        }

        public void Validate()
        {
            var errorsBuilder = new StringBuilder();

            CheckNamesAreUnique(errorsBuilder);

            if (errorsBuilder.Length > 0)
            {
                throw new InvalidOptionsDefinitionException(errorsBuilder.ToString());
            }
        }

        private void CheckNamesAreUnique(StringBuilder errorsBuilder)
        {
            var optionAliases = _optionProperties
                .Where(x => x.IsOption)
                .SelectMany(x => x.OptionAliases);

            var switchAliases = _optionProperties
                .Where(x => x.HasSwitches)
                .SelectMany(x => x.Switches)
                .SelectMany(x => x.Aliases);

            var groupedAliases = optionAliases
                .Concat(switchAliases)
                .GroupBy(x => x);

            foreach (var group in groupedAliases)
            {
                if (group.Count() > 1)
                {
                    errorsBuilder.AppendLine($"The alias '{group.Key}' is used for more than one Option or Switch.");
                }
            }
        }
    }
}