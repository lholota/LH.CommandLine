using System;
using LH.CommandLine;

namespace SingleCommandApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.AddCommandGroup("aws", awsCfg =>
            {
                awsCfg.AddCommandGroup("dynamodb", dynamoCfg =>
                {
                    dynamoCfg.AddCommand<SampleOptions, SampleCommand>("list");
                    dynamoCfg.AddCommand<SampleOptions, SampleCommand>("add");
                    dynamoCfg.AddCommand<SampleOptions, SampleCommand>("save");
                });
            });

            app.SetDefaultCommand<SampleOptions, SampleCommand>();
            app.Run(args);

            Console.ReadKey(true);
        }
    }
}