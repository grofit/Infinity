using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Infinity.Plugins.TIS;
using netextender.extensions;
using NUnit.Framework;

namespace UnitTests.Plugins
{
    [TestFixture]
    public class TISPlugin_Tests
    {
        [Test]
        public void should_correctly_read_signature()
        {
            var expectedOutput = "TIS V1  ";
            var plugin = new TISPlugin();

            var getSignatureMethod = plugin.GetType().GetMethod("ConvertToSignature", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = getSignatureMethod.Invoke(plugin, new[] { expectedOutput.AsBytes() });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_correctly_read_descriptor()
        {
            var expectedOutput = new TISDescriptor();
            expectedOutput.TileCount = 1;
            expectedOutput.TilesSize = 0x0C;
            expectedOutput.TileOffset = 0x1C;
            expectedOutput.TileDimensions = 64;

            var entriesBytes = new byte[]
                                  {
                                    // Descriptor
                                    0x01, 0x00, 0x00, 0x00,    // Tile Count int32
                                    0x0C, 0x00, 0x00, 0x00,    // Tile Section Length int32
                                    0x1C, 0x00, 0x00, 0x00,    // Tile Offset int32
                                    0x40, 0x00, 0x00, 0x00     // Tile Dimensions int32
                                  };

            var plugin = new TISPlugin();
            var memoryStream = new MemoryStream(entriesBytes);
            var binaryReader = new BinaryReader(memoryStream);

            var createDescriptorMethod = plugin.GetType().GetMethod("ReadDescriptor", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createDescriptorMethod.Invoke(plugin, new[] { binaryReader });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_correctly_read_tile()
        {
            var dummyDescriptor = new TISDescriptor();
            dummyDescriptor.TileDimensions = 2;

            var dummyColour = new TISColour { B = 255, G = 255, R = 255, A = 255 };
            var dummyPalette = new List<TISColour>();
            for (int i = 0; i < 256; i++)
            { dummyPalette.Add(dummyColour); }

            var dummyPixels = new List<byte>();
            for (byte i = 0; i < dummyDescriptor.TileDimensions * 2; i++)
            { dummyPixels.Add(i); }

            var expectedOutput = new TISTile(dummyPalette, dummyPixels, dummyDescriptor.TileDimensions);

            var plugin = new TISPlugin();

            var tileBytes = new List<byte>();
            foreach(var colour in dummyPalette)
            { tileBytes.AddRange(new[] { colour.A, colour.R, colour.G, colour.B }); }
            tileBytes.AddRange(dummyPixels);

            var memoryStream = new MemoryStream(tileBytes.ToArray());
            var binaryReader = new BinaryReader(memoryStream);

            var createTileMethod = plugin.GetType().GetMethod("ReadTile", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createTileMethod.Invoke(plugin, new object[] { binaryReader, dummyDescriptor });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_correctly_read_tiles()
        {
            var dummyDescriptor = new TISDescriptor();
            dummyDescriptor.TileCount = 2;
            dummyDescriptor.TileDimensions = 2;

            var dummyColour = new TISColour { B = 255, G = 255, R = 255, A = 255 };
            var dummyPalette = new List<TISColour>();
            for (int i = 0; i < 256; i++)
            { dummyPalette.Add(dummyColour); }

            var dummyPixels = new List<byte>();
            for (byte i = 0; i < dummyDescriptor.TileDimensions * 2; i++)
            { dummyPixels.Add(i); }

            var expectedOutput = new List<TISTile>();
            for(int i = 0; i< dummyDescriptor.TileCount; i++)
            { expectedOutput.Add(new TISTile(dummyPalette, dummyPixels, dummyDescriptor.TileDimensions)); }

            var plugin = new TISPlugin();

            var tileBytes = new List<byte>();

            for (int i = 0; i < dummyDescriptor.TileCount; i++)
            {
                foreach (var colour in dummyPalette)
                { tileBytes.AddRange(new[] {colour.A, colour.R, colour.G, colour.B}); }
                tileBytes.AddRange(dummyPixels);
            }

            var memoryStream = new MemoryStream(tileBytes.ToArray());
            var binaryReader = new BinaryReader(memoryStream);

            var createTileMethod = plugin.GetType().GetMethod("ReadTiles", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createTileMethod.Invoke(plugin, new object[] { binaryReader, dummyDescriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }
    }
}
