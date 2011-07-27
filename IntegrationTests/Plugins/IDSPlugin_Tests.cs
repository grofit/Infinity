using System;
using System.IO;
using System.Text;
using Infinity.Encryption;
using Infinity.Encryption.Xor;
using Infinity.Plugins.IDS;
using IntegrationTests.Helpers;
using NUnit.Framework;

namespace IntegrationTests.Plugins
{
    [TestFixture]
    public class IDSPlugin_Tests
    {
        private IEncryption m_DummyEncryption;

        [SetUp]
        public void SetUp()
        {
            m_DummyEncryption = new XOREncryption(new XORKey(new byte[] { 0x01 }));
        }

        [Test]
        public void should_correctly_read_well_structured_unencrypted_stream()
        {
            var dummyStream = CreateWellStructuredIDSStream(false);
            var plugin = new IDSPlugin(null);

            var expectedIDS = new IDSResource();
            expectedIDS.DefaultValue = 10;
            expectedIDS.Mappings.Add(1, "FirstValue");
            expectedIDS.Mappings.Add(2, "SecondValue");
            expectedIDS.Mappings.Add(3, "ThirdValue");
            
            var result = plugin.Import(dummyStream);

            Assert.That(result, Is.EqualTo(expectedIDS));
        }

        [Test]
        public void should_correctly_read_well_structured_encrypted_stream()
        {
            var dummyStream = CreateWellStructuredIDSStream(true);
            var plugin = new IDSPlugin(m_DummyEncryption);

            var expectedIDS = new IDSResource();
            expectedIDS.DefaultValue = 10;
            expectedIDS.Mappings.Add(1, "FirstValue");
            expectedIDS.Mappings.Add(2, "SecondValue");
            expectedIDS.Mappings.Add(3, "ThirdValue");

            var result = plugin.Import(dummyStream);

            Assert.That(result, Is.EqualTo(expectedIDS));
        }

        [Test]
        public void should_correctly_read_non_well_structured_unencrypted_stream()
        {
            var dummyStream = CreateNonWellStructuredIDSStream(false);
            var plugin = new IDSPlugin(null);

            var expectedIDS = new IDSResource();
            expectedIDS.DefaultValue = 10;
            expectedIDS.Mappings.Add(1, "FirstValue");
            expectedIDS.Mappings.Add(2, "SecondValue");
            expectedIDS.Mappings.Add(3, "ThirdValue");

            var result = plugin.Import(dummyStream);

            Assert.That(result, Is.EqualTo(expectedIDS));
        }

        [Test]
        public void should_correctly_read_non_well_structured_encrypted_stream()
        {
            var dummyStream = CreateNonWellStructuredIDSStream(true);
            var plugin = new IDSPlugin(m_DummyEncryption);

            var expectedIDS = new IDSResource();
            expectedIDS.DefaultValue = 10;
            expectedIDS.Mappings.Add(1, "FirstValue");
            expectedIDS.Mappings.Add(2, "SecondValue");
            expectedIDS.Mappings.Add(3, "ThirdValue");

            var result = plugin.Import(dummyStream);

            Assert.That(result, Is.EqualTo(expectedIDS));
        }

        private Stream CreateWellStructuredIDSStream(bool isEncrypted)
        {
            var createdIDSString = new StringBuilder();

            createdIDSString.AppendFormat("10 {0}", Environment.NewLine);
            createdIDSString.AppendFormat(" 1 FirstValue {0}", Environment.NewLine);
            createdIDSString.AppendFormat(" 2 SecondValue {0}", Environment.NewLine);
            createdIDSString.AppendFormat(" 3 ThirdValue {0}", Environment.NewLine);

            return StreamHelper.BytesToStream(createdIDSString.ToString(), isEncrypted ? m_DummyEncryption : null);
        }

        private Stream CreateNonWellStructuredIDSStream(bool isEncrypted)
        {
            var createdIDSString = new StringBuilder();

            createdIDSString.AppendFormat("10 {0}{0}{0}", Environment.NewLine);
            createdIDSString.AppendFormat(" 1          FirstValue {0}{0}", Environment.NewLine);
            createdIDSString.AppendFormat(" 2    SecondValue      {0}{0}{0}", Environment.NewLine);
            createdIDSString.AppendFormat(" 3   ThirdValue {0}{0}", Environment.NewLine);

            return StreamHelper.BytesToStream(createdIDSString.ToString(), isEncrypted ? m_DummyEncryption : null);
        }
    }
}