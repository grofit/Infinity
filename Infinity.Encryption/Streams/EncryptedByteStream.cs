using System.IO;
using netextender.streams;

namespace Infinity.Encryption.Streams
{
    public class EncryptedByteStream : ByteStream
    {
        public IEncryption Encryption { get; private set; }

        public EncryptedByteStream(Stream stream, IEncryption encryption) : base(stream)
        {
            Encryption = encryption;
        }

        public override int ReadByte()
        {
            var actualByte = base.ReadByte();
            
            if(actualByte == -1)
            { return actualByte; }

            return Encryption.DecryptByte((byte)actualByte);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var readBytes = base.Read(buffer, offset, count);

            if (readBytes > 0)
            { buffer = Encryption.DecryptBytes(buffer); }
            
            return readBytes;
        }

        public override void WriteByte(byte value)
        { base.WriteByte(Encryption.EncryptByte(value)); }

        public override void Write(byte[] buffer, int offset, int count)
        { base.Write(Encryption.EncryptBytes(buffer), offset, count); }
    }
}
