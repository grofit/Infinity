using System.Collections.Generic;
using Infinity.Encryption;
using Infinity.Encryption.Xor;

namespace IntegrationTests.Helpers
{
    public static class EncryptionHelper
    {
        public static void RegisterSimpleXOREncryption()
        {
            var defaultKey = new XORKey(new List<byte> { 0x01 });
            EncryptionFactory.RegisterEncryption(new XOREncryption(defaultKey));
        }
    }
}
