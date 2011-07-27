using System;
using System.Collections.Generic;
using Infinity.Configuration;

namespace Infinity.Plugins
{
    public class PluginFactory : IPluginFactory
    {
        private IDictionary<int, IPlugin> m_Plugins;

        public PluginFactory()
        { m_Plugins = new Dictionary<int, IPlugin>(); }

        public void RegisterPlugin(IPlugin plugin)
        { m_Plugins.Add(plugin.PluginSignature, plugin); }

        public IPlugin RequestPlugin(int pluginSignature)
        {
            if(m_Plugins.ContainsKey(pluginSignature))
            { return m_Plugins[pluginSignature]; }

            LoggingConfiguration.LogError(new Exception(string.Format("Requested Plugin [{0}] Not Found", pluginSignature)));
            return null;
        }
    }
}