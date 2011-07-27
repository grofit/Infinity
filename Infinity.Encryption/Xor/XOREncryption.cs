using System.IO;

namespace Infinity.Encryption.Xor
{
    public class XOREncryption : IEncryption
    {
        private static readonly int EncryptionFlag = 0xFF;
        private static readonly int EncryptionSize = 2;

        private readonly XORKey m_Key;

        public XOREncryption(XORKey key)
        {
            m_Key = key;
        }

        public bool IsResourceEncrypted(Stream resourceStream)
        {
            long currentPosition = resourceStream.Position;
            resourceStream.Seek(0, SeekOrigin.Begin);

            var buffer = new byte[EncryptionSize];
            resourceStream.Read(buffer, 0, EncryptionSize);
            resourceStream.Seek(currentPosition, SeekOrigin.Begin);

            return (buffer[0] == EncryptionFlag && buffer[1] == EncryptionFlag);
        }

        public byte[] DecryptBytes(byte[] encryptedBytes)
        {
            var decryptedBytes = new byte[encryptedBytes.Length];
            
            for(int i=0;i<encryptedBytes.Length;i++)
            { decryptedBytes[i] = DecryptByte(encryptedBytes[i]); }

            return decryptedBytes;
        }

        public byte[] EncryptBytes(byte[] decryptedBytes)
        {
            var encryptedBytes = new byte[decryptedBytes.Length];

            for (int i = 0; i < decryptedBytes.Length; i++)
            { encryptedBytes[i] = EncryptByte(decryptedBytes[i]); }

            return encryptedBytes;
        }

        public byte DecryptByte(byte encryptedByte)
        { return (byte)(encryptedByte ^ m_Key.GetNextByte()); }

        public byte EncryptByte(byte decryptedByte)
        { return (byte)(decryptedByte ^ m_Key.GetNextByte()); }

        public void ResetState()
        { 
            m_Key.MoveKeyPosition(0);
        }
    }
}
