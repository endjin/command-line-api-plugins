namespace BarPlugin
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;

    using SysCmdLineBase;

    public class BarCommand : IPluginCommand
    {
        public Command Command()
        {
            return new Command("bar-something", "Bar the thing.")
            {
                Handler = CommandHandler.Create(() =>
                {
                    Console.WriteLine("Bar-ing the thing!");
                })
            };
        }
    }
}