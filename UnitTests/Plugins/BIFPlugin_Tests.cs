using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Infinity.Plugins.BIF;
using netextender.extensions;
using NUnit.Framework;

namespace UnitTests.Plugins
{
    [TestFixture]
    public class BIFPlugin_Tests
    {
        [Test]
        public void should_correctly_read_file_signature()
        {
            var expectedOutput = "BIFFV1  ";
            var plugin = new BIFPlugin();

            var getSignatureMethod = plugin.GetType().GetMethod("ConvertToSignature", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = getSignatureMethod.Invoke(plugin, new[] { expectedOutput.AsBytes() });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_create_descriptor()
        {
            var expectedOutput = new BIFDescriptor();
            expectedOutput.FileCount = 0x01;
            expectedOutput.FileEntryOffset = 0x0A;
            expectedOutput.TilesetCount = 0x02;
            expectedOutput.TilesetEntryOffset = 0x0A + (sizeof(int) * 4);

            var plugin = new BIFPlugin();

            var descriptorBytes = new byte[]
                                      {
                                        0x01, 0x00, 0x00, 0x00,    // File Count
                                        0x02, 0x00, 0x00, 0x00,    // Tile Count
                                        0x0A, 0x00, 0x00, 0x00     // File Entry Offset
                                                                   // Tile Entry calculated in method
                                      };

            var memoryStream = new MemoryStream(descriptorBytes);
            var binaryReader = new BinaryReader(memoryStream);

            var createDescriptorMethod = plugin.GetType().GetMethod("ReadDescriptor", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createDescriptorMethod.Invoke(plugin, new object[] { binaryReader, 0 });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_create_file_entries()
        {
            var dummyEntry1 = new BIFFileEntry(0x1, 0x2, 0x3, 0x4, 0x5);
            var dummyEntry2 = new BIFFileEntry(0x2, 0x5, 0x3, 0x4, 0x5);
            var expectedOutput = new List<BIFFileEntry> { dummyEntry1, dummyEntry2 };

            var plugin = new BIFPlugin();
            var bifDescriptor = new BIFDescriptor();
            bifDescriptor.FileCount = 2;
            bifDescriptor.FileEntryOffset = 0;

            var entriesBytes = new byte[]
                                  {
                                    // Entry 1
                                    0x01, 0x00, 0x00, 0x00,    // Locator int32
                                    0x02, 0x00, 0x00, 0x00,    // Offset int32
                                    0x03, 0x00, 0x00, 0x00,    // Size int32
                                    0x04, 0x00,                // Type int16
                                    0x05, 0x00,                // Unknown int16     
                                    // Entry 2
                                    0x02, 0x00, 0x00, 0x00,    // Locator int32
                                    0x05, 0x00, 0x00, 0x00,    // Offset int32
                                    0x03, 0x00, 0x00, 0x00,    // Size int32
                                    0x04, 0x00,                // Type int16
                                    0x05, 0x00,                // Unknown int16     
                                  };

            var memoryStream = new MemoryStream(entriesBytes);
            var binaryReader = new BinaryReader(memoryStream);

            var createFileEntriesMethod = plugin.GetType().GetMethod("ReadFileEntries", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createFileEntriesMethod.Invoke(plugin, new object[] { binaryReader, bifDescriptor });

            Assert.That(result, Is.TypeOf(typeof(List<BIFFileEntry>)));

            CollectionAssert.AreEquivalent((List<BIFFileEntry>)result, expectedOutput);
        }

        [Test]
        public void should_create_tile_entries()
        {
            var dummyEntry1 = new BIFTilesetEntry(0x1, 0x2, 0x3, 0x4, 0x5, 0x6);
            var dummyEntry2 = new BIFTilesetEntry(0x2, 0x5, 0x3, 0x4, 0x5, 0x6);
            var expectedOutput = new List<BIFTilesetEntry> { dummyEntry1, dummyEntry2 };

            var plugin = new BIFPlugin();
            var bifDescriptor = new BIFDescriptor();
            bifDescriptor.TilesetCount = 2;
            bifDescriptor.TilesetEntryOffset = 0;

            var entriesBytes = new byte[]
                                  {
                                    // Entry 1
                                    0x01, 0x00, 0x00, 0x00,    // Locator int32
                                    0x02, 0x00, 0x00, 0x00,    // Offset int32
                                    0x03, 0x00, 0x00, 0x00,    // Count int32
                                    0x04, 0x00, 0x00, 0x00,    // Size int32
                                    0x05, 0x00,                // Type int16
                                    0x06, 0x00,                // Unknown int16     
                                    // Entry 2
                                    0x02, 0x00, 0x00, 0x00,    // Locator int32
                                    0x05, 0x00, 0x00, 0x00,    // Offset int32
                                    0x03, 0x00, 0x00, 0x00,    // Count int32
                                    0x04, 0x00, 0x00, 0x00,    // Size int32
                                    0x05, 0x00,                // Type int16
                                    0x06, 0x00,                // Unknown int16     
                                  };

            var memoryStream = new MemoryStream(entriesBytes);
            var binaryReader = new BinaryReader(memoryStream);

            var createTileEntriesMethod = plugin.GetType().GetMethod("ReadTileEntries", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createTileEntriesMethod.Invoke(plugin, new object[] { binaryReader, bifDescriptor });

            Assert.That(result, Is.TypeOf(typeof(List<BIFTilesetEntry>)));

            CollectionAssert.AreEquivalent((List<BIFTilesetEntry>)result, expectedOutput);
        }

    }
}
