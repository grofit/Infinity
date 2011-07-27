using System;
using Infinity.Injection.Modules;
using Ninject;

namespace Infinity.Tools.Configuration
{
    public class DependencyInjectionConfiguration : IConfiguration
    {
        private static DependencyInjectionConfiguration m_Instance;
        public static DependencyInjectionConfiguration Instance
        {
            get
            {
                if(m_Instance == null)
                { m_Instance = new DependencyInjectionConfiguration(); }
                return m_Instance;
            }
        }

        public IKernel Kernel { get; private set; }

        private DependencyInjectionConfiguration(){}

        public void Configure()
        { Kernel = new StandardKernel(new InjectionModule()); }
    }
}
