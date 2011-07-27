using System.IO;
using System.Collections.Generic;
using Infinity.Encryption;
using Infinity.Encryption.Streams;
using Infinity.Encryption.Xor;
using NUnit.Framework;

namespace UnitTests.Streams
{
    [TestFixture]
    public class EncryptedByteStream_Tests
    {
        [Test]
        public void should_read_decrypted_bytes()
        {
            var expectedBytes = new byte[] { 0x0, 0x03, 0x02 };
            var givenBytes = new byte[] { 0x01, 0x01, 0x01 };
            var encryption = CreateDefaultEncryption();

            Stream memoryStream = new MemoryStream(encryption.EncryptBytes(givenBytes));
            encryption.ResetState();

            var byteStream = new EncryptedByteStream(memoryStream, encryption);

            var byteBuffer = new byte[givenBytes.Length];
            byteStream.Read(byteBuffer, 0, givenBytes.Length);

            Assert.That(byteBuffer, Is.EqualTo(expectedBytes));
        }

        [Test]
        public void should_read_decrypted_byte()
        {
            byte expectedByte = 0x01;
            byte givenByte = 0x01;
            var encryption = CreateDefaultEncryption();

            Stream memoryStream = new MemoryStream(new byte[] { encryption.EncryptByte(givenByte) });
            encryption.ResetState();

            var byteStream = new EncryptedByteStream(memoryStream, encryption);
            var actualByte = (byte)byteStream.ReadByte();

            Assert.That(actualByte, Is.EqualTo(expectedByte));
        }

        [Test]
        public void should_not_decrypt_eol()
        {
            int expectedByte = -1;
            var encryption = CreateDefaultEncryption();

            Stream memoryStream = new MemoryStream();
            encryption.ResetState();

            var byteStream = new EncryptedByteStream(memoryStream, encryption);
            var actualByte = byteStream.ReadByte();

            Assert.That(actualByte, Is.EqualTo(expectedByte));
        }

        [Test]
        public void should_write_encrypted_byte()
        {
            byte expectedByte = 0x00;
            byte givenByte = 0x01;
            var encryption = CreateDefaultEncryption();

            Stream memoryStream = new MemoryStream();

            var byteStream = new EncryptedByteStream(memoryStream, encryption);
            byteStream.WriteByte(givenByte);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var actualByte = memoryStream.ReadByte();
            Assert.That(actualByte, Is.EqualTo(expectedByte));
        }

        [Test]
        public void should_write_encrypted_bytes()
        {
            var expectedBytes = new byte[] { 0x0, 0x03, 0x02 };
            var givenBytes = new byte[] { 0x01, 0x01, 0x01 };
            var encryption = CreateDefaultEncryption();

            Stream memoryStream = new MemoryStream();

            var byteStream = new EncryptedByteStream(memoryStream, encryption);
            byteStream.Write(givenBytes, 0, givenBytes.Length);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var byteBuffer = new byte[givenBytes.Length];
            memoryStream.Read(byteBuffer, 0, byteBuffer.Length);

            Assert.That(byteBuffer, Is.EqualTo(expectedBytes));
        }

        private IEncryption CreateDefaultEncryption()
        {
            // Rotates key on every action, unless state is reset
            var key = new XORKey(new List<byte> { 0x01, 0x02, 0x03 });
            return new XOREncryption(key);
        }
    }
}


/*
public override int ReadByte()
        {
            var actualByte = base.ReadByte();
            return Encryption.DecryptByte((byte)actualByte);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var readBytes = base.Read(buffer, offset, count);
            buffer = Encryption.DecryptBytes(buffer);
            return readBytes;
        }

        public override void WriteByte(byte value)
        { base.WriteByte(Encryption.EncryptByte(value)); }

        public override void Write(byte[] buffer, int offset, int count)
        { base.Write(Encryption.EncryptBytes(buffer), offset, count); }
*/