using System.IO;

namespace Infinity.Encryption
{
    public interface IEncryption
    {
        bool IsResourceEncrypted(Stream stream);

        byte DecryptByte(byte encryptedByte);
        byte EncryptByte(byte decryptedByte);

        byte[] DecryptBytes(byte[] encryptedBytes);
        byte[] EncryptBytes(byte[] decryptedBytes);

        void ResetState();
    }
}
