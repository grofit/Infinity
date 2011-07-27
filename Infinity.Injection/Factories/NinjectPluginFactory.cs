using Infinity.Plugins;
using Ninject;

namespace Infinity.Injection.Factories
{
    public class NinjectPluginFactory : IPluginFactory
    {
        public NinjectPluginFactory()
        {}

        public NinjectPluginFactory(params IPlugin[] plugins)
        {
            foreach(var plugin in plugins)
            { RegisterPlugin(plugin); }
        }

        public void RegisterPlugin(IPlugin plugin)
        {
            PluginKernel.Kernel.Bind<IPlugin>()
                               .To(plugin.GetType())
                               .Named(plugin.PluginSignature.ToString());
        }

        public IPlugin RequestPlugin(int pluginSignature)
        {
            return PluginKernel.Kernel.Get<IPlugin>(pluginSignature.ToString());
        }
    }
}