namespace SysCmdLineBase
{
    using System.CommandLine;

    public interface IPluginCommand
    {
        Command Command();
    }
}