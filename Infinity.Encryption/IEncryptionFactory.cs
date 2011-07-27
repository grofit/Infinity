namespace Infinity.Encryption
{
    public interface IEncryptionFactory
    {
        void RegisterEncryption(IEncryption encryption);
        T RequestEncryptor<T>() where T : class, IEncryption;
    }
}