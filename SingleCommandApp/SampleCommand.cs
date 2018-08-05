using System;
using LH.CommandLine;

namespace SingleCommandApp
{
    public class SampleCommand : ICommand<SampleOptions>
    {
        public void Execute(SampleOptions sampleOptions)
        {
            throw new NotImplementedException();
        }
    }
}
