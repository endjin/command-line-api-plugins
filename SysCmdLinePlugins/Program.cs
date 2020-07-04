namespace SysCmdLinePlugins
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.Builder;
    using System.CommandLine.Invocation;
    using System.CommandLine.Parsing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using McMaster.NETCore.Plugins;

    using NDepend.Path;

    using SysCmdLineBase;

    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var loaders = new List<PluginLoader>();

            var dirs = new List<IRelativeDirectoryPath>
            {
                @".\FooPlugin\bin\Debug\netcoreapp3.1\".ToRelativeDirectoryPath(),
                @".\BarPlugin\bin\Debug\netcoreapp3.1\".ToRelativeDirectoryPath()
            };

            var commonDir = AppContext.BaseDirectory.ToAbsoluteDirectoryPath().ParentDirectoryPath
                                                                              .ParentDirectoryPath
                                                                              .ParentDirectoryPath
                                                                              .ParentDirectoryPath;

            foreach (var dir in dirs)
            {
                foreach (var child in dir.GetAbsolutePathFrom(commonDir).ChildrenFilesPath.Where(x => x.FileExtension == ".dll"))
                {
                    var loader = PluginLoader.CreateFromAssemblyFile(child.FileInfo.FullName, sharedTypes: new[] { typeof(IPluginCommand) });

                    loaders.Add(loader);
                }
            }

            var cmd = new CommandLineBuilder();

            foreach (var loader in loaders)
            {
                foreach (var pluginType in loader
                    .LoadDefaultAssembly()
                    .GetTypes()
                    .Where(t => typeof(IPluginCommand).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    // This assumes the implementation of IPlugin has a parameterless constructor
                    var plugin = Activator.CreateInstance(pluginType) as IPluginCommand;

                    cmd.AddCommand(plugin?.Command());
                }
            }

            var parser = cmd.UseDefaults().Build();

            return await parser.InvokeAsync(args).ConfigureAwait(false);
        }
    }
}