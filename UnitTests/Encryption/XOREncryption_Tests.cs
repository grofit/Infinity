using System.Collections.Generic;
using System.IO;
using Infinity.Encryption.Xor;
using NUnit.Framework;
using Rhino.Mocks;

namespace UnitTests.Encryption
{
    [TestFixture]
    public class XOREncryption_Tests
    {
        [Test]
        public void should_correctly_read_encryption_flag()
        {
            byte[] encryptionBytes = { 0xFF, 0xFF };
            var key = new XORKey(new List<byte> { 0x01 });

            var mockStream = MockRepository.GenerateMock<Stream>();

            var encryption = new XOREncryption(key);

            mockStream.Stub(e => e.Read(
                                     Arg<byte[]>.Out(encryptionBytes).Dummy,
                                     Arg<int>.Is.Equal(0),
                                     Arg<int>.Is.Equal(2))).Return(0);

            var result = encryption.IsResourceEncrypted(mockStream);

            Assert.That(result, Is.True);
        }

        [Test]
        public void should_correctly_decrypt_byte()
        {
            byte expectedByte = 0x03;
            byte givenByte = 0x02;

            var key = new XORKey(new List<byte>{0x01});

            var encryption = new XOREncryption(key);
            var actualByte = encryption.DecryptByte(givenByte);

            Assert.That(actualByte, Is.EqualTo(expectedByte));
        }
        
        [Test]
        public void should_correctly_decrypt_bytes()
        {
            var expectedBytes = new byte[] { 0x0, 0x03, 0x02 };
            var givenBytes = new byte[] { 0x01, 0x01, 0x01 };

            var key = new XORKey(new List<byte> { 0x01, 0x02, 0x03 });

            var encryption = new XOREncryption(key);
            var actualBytes = encryption.DecryptBytes(givenBytes);

            Assert.That(actualBytes, Is.EqualTo(expectedBytes));
        }

        [Test]
        public void should_correctly_encrypt_byte()
        {
            byte expectedByte = 0x02;
            byte givenByte = 0x03;

            var key = new XORKey(new List<byte> { 0x01 });

            var encryption = new XOREncryption(key);
            var actualByte = encryption.EncryptByte(givenByte);

            Assert.That(actualByte, Is.EqualTo(expectedByte));
        }

        [Test]
        public void should_correctly_encrypt_bytes()
        {
            var expectedBytes = new byte[] { 0x01, 0x01, 0x01 };
            var givenBytes = new byte[] { 0x0, 0x03, 0x02 };

            var key = new XORKey(new List<byte> { 0x01, 0x02, 0x03 });

            var encryption = new XOREncryption(key);
            var actualBytes = encryption.EncryptBytes(givenBytes);

            Assert.That(actualBytes, Is.EqualTo(expectedBytes));
        }
    }
}
