using System;
using Infinity.Encryption;
using Ninject;

namespace Infinity.Injection.Factories
{
    public class NinjectEncryptionFactory : IEncryptionFactory
    {
         public NinjectEncryptionFactory()
        {}

         public NinjectEncryptionFactory(params IEncryption[] encryptions)
        {
            foreach(var plugin in encryptions)
            { RegisterEncryption(plugin); }
        }

        public void RegisterEncryption(IEncryption encryption)
        {
            Type encryptionType = encryption.GetType();
            PluginKernel.Kernel.Bind<IEncryption>()
                .To(encryptionType)
                .Named(encryptionType.Name);
        }

        public T RequestEncryptor<T>() where T : class, IEncryption
        { return PluginKernel.Kernel.Get<T>(typeof (T).Name); }
    }
}