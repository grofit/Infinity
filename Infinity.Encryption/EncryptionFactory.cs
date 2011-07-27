using System;
using System.Collections.Generic;
using Infinity.Configuration;

namespace Infinity.Encryption
{
    public class EncryptionFactory : IEncryptionFactory
    {
        private IDictionary<Type, IEncryption> m_Encryptors;

        public EncryptionFactory()
        { m_Encryptors = new Dictionary<Type, IEncryption>(); }

        public void RegisterEncryption(IEncryption encryption)
        {
            if(m_Encryptors.ContainsKey(encryption.GetType()))
            { m_Encryptors[encryption.GetType()] = encryption; }
            else
            { m_Encryptors.Add(encryption.GetType(), encryption); }
        }

        public T RequestEncryptor<T>() where T : class, IEncryption
        {
            if (m_Encryptors.ContainsKey(typeof(T)))
            { return (m_Encryptors[typeof(T)] as T); }

            LoggingConfiguration.LogError(new Exception(string.Format("Requested Encryption [{0}] Not Found", typeof(T))));
            return null;
        }
        
    }
}
