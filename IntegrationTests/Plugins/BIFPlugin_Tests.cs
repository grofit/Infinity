using System.IO;
using Infinity.Plugins.BIF;
using NUnit.Framework;

namespace IntegrationTests.Plugins
{
    [TestFixture]
    public class BIFPlugin_Tests
    {
        [Test]
        public void should_correctly_read_bif_stream()
        {
            var dummyStream = CreateDummyBIFStream();
            var plugin = new BIFPlugin();

            var expectedBIF = new BIFResource();

            expectedBIF.FileEntries.Add(new BIFFileEntry(0x1, 0x2, 0x3, 0x4, 0x5));
            expectedBIF.FileEntries.Add(new BIFFileEntry(0x6, 0x7, 0x8, 0x9, 0xA));

            expectedBIF.TilesetEntries.Add(new BIFTilesetEntry(0x1,0x2,0x3,0x4,0x5,0x6));
            expectedBIF.TilesetEntries.Add(new BIFTilesetEntry(0x7,0x8,0x9,0xA,0xB,0xC));

            var result = plugin.Import(dummyStream);

            Assert.That(result, Is.EqualTo(expectedBIF));
        }

        private Stream CreateDummyBIFStream()
        {
            var byteBuffer = new byte[]
                                {
                                    // Signature
                                    0x42, 0x49, 0x46, 0x46, 0x56, 0x31, 0x20, 0x20, // BIFFV1__
                                    // Descriptor
                                    0x02, 0x00, 0x00, 0x00,    // File Count
                                    0x02, 0x00, 0x00, 0x00,    // Tile Count
                                    0x14, 0x00, 0x00, 0x00,    // File Entry Offset
                                    // File Entry 1
                                    0x01, 0x00, 0x00, 0x00,    // Locator int32
                                    0x02, 0x00, 0x00, 0x00,    // Offset int32
                                    0x03, 0x00, 0x00, 0x00,    // Size int32
                                    0x04, 0x00,                // Type int16
                                    0x05, 0x00,                // Unknown int16
                                    // File Entry 2
                                    0x06, 0x00, 0x00, 0x00,    // Locator int32
                                    0x07, 0x00, 0x00, 0x00,    // Offset int32
                                    0x08, 0x00, 0x00, 0x00,    // Size int32
                                    0x09, 0x00,                // Type int16
                                    0x0A, 0x00,                // Unknown int16
                                    // Tile Entry 1
                                    0x01, 0x00, 0x00, 0x00,    // Locator int32
                                    0x02, 0x00, 0x00, 0x00,    // Offset int32
                                    0x03, 0x00, 0x00, 0x00,    // Count int32
                                    0x04, 0x00, 0x00, 0x00,    // Size int32
                                    0x05, 0x00,                // Type int16
                                    0x06, 0x00,                // Unknown int16
                                    // Tile Entry 2
                                    0x07, 0x00, 0x00, 0x00,    // Locator int32
                                    0x08, 0x00, 0x00, 0x00,    // Offset int32
                                    0x09, 0x00, 0x00, 0x00,    // Count int32
                                    0x0A, 0x00, 0x00, 0x00,    // Size int32
                                    0x0B, 0x00,                // Type int16
                                    0x0C, 0x00                 // Unknown int16
                                };

            return new MemoryStream(byteBuffer);
        }
    }
}
