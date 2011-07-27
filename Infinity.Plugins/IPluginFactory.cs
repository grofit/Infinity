namespace Infinity.Plugins
{
    public interface IPluginFactory
    {
        void RegisterPlugin(IPlugin plugin);
        IPlugin RequestPlugin(int pluginSignature);
    }
}
