using System.IO;
using Infinity.Encryption;
using netextender.extensions;

namespace IntegrationTests.Helpers
{
    public static class StreamHelper
    {
        public static Stream BytesToStream(string data, IEncryption encryption)
        {
            var convertedBytes = data.AsBytes();
            byte[] createdBuffer;

            if (encryption != null)
            {
                createdBuffer = new byte[convertedBytes.Length + 2];
                createdBuffer[0] = createdBuffer[1] = 0xFF;
                var encryptedBuffer = encryption.EncryptBytes(convertedBytes);
                encryptedBuffer.CopyTo(createdBuffer, 2);
            }
            else
            { createdBuffer = convertedBytes; }

            Stream memoryStream = new MemoryStream();
            memoryStream.Write(createdBuffer, 0, createdBuffer.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}
