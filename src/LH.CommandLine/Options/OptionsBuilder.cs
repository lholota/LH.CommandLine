//using System;
//using System.Collections.Generic;
//using System.Linq;
//using LH.CommandLine.Options.Builders;

//namespace LH.CommandLine.Options.Factoring
//{
//    internal class OptionsBuilder<TOptions>
//    {
//        private readonly OptionsFactory<TOptions> _optionsFactory;
//        private readonly IReadOnlyList<OptionProperty> _properties;

//        public OptionsBuilder()
//        {
//            _optionsFactory = new OptionsFactory<TOptions>(); ;
//            _properties = OptionProperty.GetOptionProperties(typeof(TOptions));
//        }

//        public bool TrySetPositionalArgument(int index, string value)
//        {
//            var optionProperty = _properties.SingleOrDefault(x => x.IsPositional && x.PositionalIndex == index);

//            if (optionProperty == null)
//            {
//                return false;
//            }

//            optionProperty.SetValue(value);
//            return true;
//        }

//        public bool TrySetOption(string name, string value)
//        {
//            var optionProperty = _properties.SingleOrDefault(x => x.IsNamed && x.Aliases.Contains(name));

//            if (optionProperty == null)
//            {
//                return false;
//            }

//            optionProperty.SetValue(value);
//            return true;
//        }

//        public bool TrySetSwitch(string name)
//        {
//            var optionProperty = _properties.SingleOrDefault(x => x.IsNamed && x.IsSwitch && x.Aliases.Contains(name));

//            if (optionProperty == null)
//            {
//                return false;
//            }

//            optionProperty.SetSwitchValue();
//            return true;
//        }

//        public TOptions Build()
//        {
//            return _optionsFactory.CreateOptions(_properties);
//        }
//    }
//}