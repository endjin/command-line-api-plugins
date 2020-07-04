namespace FooPlugin
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;

    using SysCmdLineBase;

    public class FooCommand : IPluginCommand
    {
        public Command Command()
        {
            return new Command("foo-something", "Foo the thing.")
            {
                Handler = CommandHandler.Create(() =>
                {
                    Console.WriteLine("Foo-ing the thing!");
                })
            };
        }
    }
}