using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Infinity.Encryption;
using Infinity.Encryption.Xor;
using Infinity.Plugins.TwoDA;
using IntegrationTests.Helpers;
using NUnit.Framework;

namespace IntegrationTests.Plugins
{
    [TestFixture]
    public class TwoDAPlugin_Tests
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
            var dummyStream = CreateWellStructured2DAStream(false);
            var plugin = new TwoDAPlugin(null);
            
            var expected2DA = new TwoDAResource();
            expected2DA.DefaultValue = "10";
            expected2DA.Columns = new List<string> { "col1", "col2", "col3", "col4" };
            expected2DA.Rows.Add(new TwoDARow("row1", new List<string> { "1", "2", "3", "4" }));
            expected2DA.Rows.Add(new TwoDARow("row2", new List<string> { "2", "4", "6", "8" }));
            expected2DA.Rows.Add(new TwoDARow("row3", new List<string> { "3", "6", "9", "12" }));

            var result = plugin.Import(dummyStream);

            Assert.That(result, Is.EqualTo(expected2DA));
        }

        [Test]
        public void should_correctly_read_well_structured_encrypted_stream()
        {
            var dummyStream = CreateWellStructured2DAStream(true);
            var plugin = new TwoDAPlugin(m_DummyEncryption);

            var expected2DA = new TwoDAResource();
            expected2DA.DefaultValue = "10";
            expected2DA.Columns = new List<string> { "col1", "col2", "col3", "col4" };
            expected2DA.Rows.Add(new TwoDARow("row1", new List<string> { "1", "2", "3", "4" }));
            expected2DA.Rows.Add(new TwoDARow("row2", new List<string> { "2", "4", "6", "8" }));
            expected2DA.Rows.Add(new TwoDARow("row3", new List<string> { "3", "6", "9", "12" }));

            var result = plugin.Import(dummyStream);

            Assert.That(result, Is.EqualTo(expected2DA));
        }

        [Test]
        public void should_correctly_read_non_well_structured_unencrypted_stream()
        {
            var dummyStream = CreateNonWellStructured2DAStream(false);
            var plugin = new TwoDAPlugin(null);

            var expected2DA = new TwoDAResource();
            expected2DA.DefaultValue = "10";
            expected2DA.Columns = new List<string> { "col1", "col2", "col3", "col4" };
            expected2DA.Rows.Add(new TwoDARow("row1", new List<string> { "1", "2", "3", "4" }));
            expected2DA.Rows.Add(new TwoDARow("row2", new List<string> { "2", "4", "6", "8" }));
            expected2DA.Rows.Add(new TwoDARow("row3", new List<string> { "3", "6", "9", "12" }));

            var result = plugin.Import(dummyStream);

            Assert.That(result, Is.EqualTo(expected2DA));
        }

        [Test]
        public void should_correctly_read_non_well_structured_encrypted_stream()
        {
            var dummyStream = CreateNonWellStructured2DAStream(true);
            var plugin = new TwoDAPlugin(m_DummyEncryption);

            var expected2DA = new TwoDAResource();
            expected2DA.DefaultValue = "10";
            expected2DA.Columns = new List<string> { "col1", "col2", "col3", "col4" };
            expected2DA.Rows.Add(new TwoDARow("row1", new List<string> { "1", "2", "3", "4" }));
            expected2DA.Rows.Add(new TwoDARow("row2", new List<string> { "2", "4", "6", "8" }));
            expected2DA.Rows.Add(new TwoDARow("row3", new List<string> { "3", "6", "9", "12" }));

            var result = plugin.Import(dummyStream);

            Assert.That(result, Is.EqualTo(expected2DA));
        }

        private Stream CreateWellStructured2DAStream(bool isEncrypted)
        {
            var created2DAString = new StringBuilder();
            
            var signature = "2DA V1.0";

            created2DAString.AppendFormat("{0} {1}", signature, Environment.NewLine);
            created2DAString.AppendFormat("10 {0}", Environment.NewLine);
            created2DAString.AppendFormat("      col1 col2 col3 col4 {0}", Environment.NewLine);
            created2DAString.AppendFormat(" row1 1    2    3    4    {0}", Environment.NewLine);
            created2DAString.AppendFormat(" row2 2    4    6    8    {0}", Environment.NewLine);
            created2DAString.AppendFormat(" row3 3    6    9    12   {0}", Environment.NewLine);

            return StreamHelper.BytesToStream(created2DAString.ToString(), isEncrypted ? m_DummyEncryption : null);
        }

        private Stream CreateNonWellStructured2DAStream(bool isEncrypted)
        {
            var created2DAString = new StringBuilder();

            var signature = "2DA V1.0";

            created2DAString.AppendFormat("{0} {1}{1}{1}", signature, Environment.NewLine);
            created2DAString.AppendFormat("    10         {0}{0}", Environment.NewLine);
            created2DAString.AppendFormat("         col1     col2  col3     col4 {0}{0}{0}", Environment.NewLine);
            created2DAString.AppendFormat(" row1    1       2    3    4    {0}{0}", Environment.NewLine);
            created2DAString.AppendFormat(" row2   2      4      6    8    {0}", Environment.NewLine);
            created2DAString.AppendFormat(" row3    3      6 9    12   {0}{0}{0}", Environment.NewLine);

            return StreamHelper.BytesToStream(created2DAString.ToString(), isEncrypted ? m_DummyEncryption : null);
        }
    }
}