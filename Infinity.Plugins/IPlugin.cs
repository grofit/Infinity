namespace Infinity
{
    public interface IPlugin
    {
        int PluginSignature { get; }
        string TargetVersion { get; }
    }
}