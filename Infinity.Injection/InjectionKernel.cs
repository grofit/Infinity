using Ninject;

namespace Infinity.Injection
{
    public static class InjectionKernel
    {
        private static IKernel m_Kernel;

        public static IKernel Kernel
        {
            get
            {
                if(m_Kernel == null)
                { m_Kernel = new StandardKernel(); }
                return m_Kernel;
            } 
            set
            { m_Kernel = value; }
        }
    }
}
