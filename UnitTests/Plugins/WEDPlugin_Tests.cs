using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Infinity.Plugins.WED;
using netextender.extensions;
using NUnit.Framework;

namespace UnitTests.Plugins
{
    [TestFixture]
    public class WEDPlugin_Tests
    {
        [Test]
        public void should_correctly_read_signature()
        {
            var expectedOutput = "WED V1  ";
            var plugin = new WEDPlugin();

            var getSignatureMethod = plugin.GetType().GetMethod("ConvertToSignature", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = getSignatureMethod.Invoke(plugin, new[] { expectedOutput.AsBytes() });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_correctly_read_the_initial_descriptor()
        {
            var expectedOutput = new WEDDescriptor();
            expectedOutput.OverlayCount = 10;
            expectedOutput.OverlayOffset = 0x0C;
            expectedOutput.SecondaryDescriptorOffset = 0x1C;
            expectedOutput.DoorTileOffset = 0x3D;
            expectedOutput.DoorCount = 5;
            expectedOutput.DoorOffset = 0x2D;

            var plugin = new WEDPlugin();

            var entriesBytes = new byte[]
                                  {
                                    // Descriptor
                                    0x0A, 0x00, 0x00, 0x00,    // Overlay Count int32
                                    0x05, 0x00, 0x00, 0x00,    // Doors Count int32
                                    0x0C, 0x00, 0x00, 0x00,    // Overlay Offset int32
                                    0x1C, 0x00, 0x00, 0x00,    // Secondary Descriptor Offset int32
                                    0x2D, 0x00, 0x00, 0x00,    // Doors Offset int32
                                    0x3D, 0x00, 0x00, 0x00     // Door Tile Offset int32      
                                  };

            var memoryStream = new MemoryStream(entriesBytes);
            var binaryReader = new BinaryReader(memoryStream);

            var createDescriptorMethod = plugin.GetType().GetMethod("ReadDescriptor", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createDescriptorMethod.Invoke(plugin, new[] { binaryReader });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_correctly_read_overlays()
        {
            var expectedOutput = new List<WEDOverlay>();
            var overlay1 = new WEDOverlay();
            overlay1.Width = 32;
            overlay1.Height = 32;
            overlay1.ResourceType = "example1".ToCharArray();
            overlay1.UnknownData = 0x3D;
            overlay1.TileOffset = 5;
            overlay1.TileIndexOffset = 0x2D;
            var overlay2 = new WEDOverlay();
            overlay2.Width = 32;
            overlay2.Height = 32;
            overlay2.ResourceType = "example2".ToCharArray();
            overlay2.UnknownData = 0xC1;
            overlay2.TileOffset = 2;
            overlay2.TileIndexOffset = 0xFD;

            expectedOutput.Add(overlay1);
            expectedOutput.Add(overlay2);

            var plugin = new WEDPlugin();

            var memoryStream = new MemoryStream();
            memoryStream.WriteStruct(overlay1);
            memoryStream.WriteStruct(overlay2);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var descriptor = new WEDDescriptor();
            descriptor.OverlayOffset = 0;
            descriptor.OverlayCount = 2;

            var createDescriptorMethod = plugin.GetType().GetMethod("ReadOverlays", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createDescriptorMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_create_secondary_descriptor()
        {
            var expectedOutput = new WEDSecondDescriptor();
            expectedOutput.PolygonCount = 2;
            expectedOutput.PolygonOffset = 0x0D;
            expectedOutput.VerticeOffset = 0x1D;
            expectedOutput.WallGroupOffset = 0x2D;
            expectedOutput.IndiciesOffset = 0x3D;

            var plugin = new WEDPlugin();

            var entriesBytes = new byte[]
                                  {
                                    // Secondary Descriptor
                                    0x02, 0x00, 0x00, 0x00,     // Polygon Count int32
                                    0x0D, 0x00, 0x00, 0x00,     // Polygon Offset int32
                                    0x1D, 0x00, 0x00, 0x00,     // Vertice Offset int32
                                    0x2D, 0x00, 0x00, 0x00,     // Wall Offset int32
                                    0x3D, 0x00, 0x00, 0x00      // Indicies Offset int32      
                                  };

            var descriptor = new WEDDescriptor();
            descriptor.SecondaryDescriptorOffset = 0x0;

            var memoryStream = new MemoryStream(entriesBytes);
            var binaryReader = new BinaryReader(memoryStream);

            var createDescriptorMethod = plugin.GetType().GetMethod("ReadSecondaryDescriptor", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createDescriptorMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_correctly_read_doors()
        {           
            var door1 = new WEDDoor();
            door1.Name = "example1".ToCharArray();
            door1.IsOpen = 0x01;
            door1.TileCellIndex = 0x1D;
            door1.TileCellCount = 2;
            door1.DoorOpenPolygonCount = 0x01;
            door1.DoorClosedPolygonCount = 0x01;
            door1.DoorOpenPolygonOffset = 0x1D;
            door1.DoorClosedPolygonOffset = 0x2D;

            var door2 = new WEDDoor();
            door2.Name = "example2".ToCharArray();
            door2.IsOpen = 0x0;
            door2.TileCellIndex = 0x2D;
            door2.TileCellCount = 20;
            door2.DoorOpenPolygonCount = 0x01;
            door2.DoorClosedPolygonCount = 0x01;
            door2.DoorOpenPolygonOffset = 0x3D;
            door2.DoorClosedPolygonOffset = 0x4D;

            var expectedOutput = new List<WEDDoor>() {door1, door2};

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var descriptor = new WEDDescriptor();
            descriptor.DoorOffset = 0;
            descriptor.DoorCount = 2;
            
            var plugin = new WEDPlugin();

            var createDoorsMethod = plugin.GetType().GetMethod("ReadDoors", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createDoorsMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_correctly_read_tilemaps()
        {
            var sizeOfTilemapStructure = 10;

            var firstTilemap = new WEDTilemap();
            firstTilemap.TilesetStartIndex = 1;
            firstTilemap.TilesetCount = 5;
            firstTilemap.SecondaryTilesetIndex = 1;
            firstTilemap.OverlayToDraw = 0x1;

            var secondTilemap = new WEDTilemap();
            secondTilemap.TilesetStartIndex = 2;
            secondTilemap.TilesetCount = 32;
            secondTilemap.SecondaryTilesetIndex = 3;
            secondTilemap.OverlayToDraw = 0x2;

            var expectedOutput = new List<WEDTilemap>() {firstTilemap, secondTilemap};
            var dummyOverlays = new List<WEDOverlay>()
                                    {
                                        new WEDOverlay() {TileOffset = 0},
                                        new WEDOverlay() {TileOffset = sizeOfTilemapStructure}
                                    };

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var plugin = new WEDPlugin();

            var createDoorsMethod = plugin.GetType().GetMethod("ReadTilemaps", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createDoorsMethod.Invoke(plugin, new object[] { binaryReader, dummyOverlays });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        [Ignore]
        public void should_correctly_read_wallgroups()
        {
            throw new Exception("Not implemented correctly yet");
        }
    }
}
