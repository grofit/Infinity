using System;
using System.Collections.Generic;
using System.IO;
using Infinity.Configuration;
using Infinity.Lookups;
using netextender.extensions;

namespace Infinity.Plugins.WED
{
    public class WEDPlugin : IPlugin
    {
        public int PluginSignature
        {
            get { return ResourceTypes.WED; }
        }

        public string TargetVersion
        {
            get { return "1.0"; }
        }

        private static readonly string SignatureWED = "WED V1  ";

        public void Import(Stream stream)
        {
            var binaryReader = new BinaryReader(stream);

            var signature = ConvertToSignature(binaryReader.ReadBytes(SignatureWED.Length));
            if (!signature.Equals(SignatureWED, StringComparison.CurrentCultureIgnoreCase))
            { LoggingConfiguration.LogAndThrowError(new Exception("Invalid signature for WED file")); }

            var descriptor = ReadDescriptor(binaryReader);
            var overlays = ReadOverlays(binaryReader, descriptor);
            var doors = ReadDoors(binaryReader, descriptor);
            var secondDescriptor = ReadSecondaryDescriptor(binaryReader, descriptor);
            var tilemaps = ReadTilemaps(binaryReader, overlays);
            var wallgroups = ReadWallgroups(binaryReader, secondDescriptor);
            
        }

        private string ConvertToSignature(byte[] bytes)
        { return bytes.AsString(); }

        private WEDDescriptor ReadDescriptor(BinaryReader reader)
        { return reader.ReadStruct<WEDDescriptor>(); }


        private IList<WEDOverlay> ReadOverlays(BinaryReader reader, WEDDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.OverlayOffset, SeekOrigin.Begin);
            return reader.ReadStructs<WEDOverlay>(descriptor.OverlayCount);
        }

        private WEDSecondDescriptor ReadSecondaryDescriptor(BinaryReader reader, WEDDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.SecondaryDescriptorOffset, SeekOrigin.Begin);
            return reader.ReadStruct<WEDSecondDescriptor>();
        }

        private IList<WEDDoor> ReadDoors(BinaryReader reader, WEDDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.DoorOffset, SeekOrigin.Begin);
            return reader.ReadStructs<WEDDoor>(descriptor.DoorCount);
        }

        private IList<WEDTilemap> ReadTilemaps(BinaryReader reader, IEnumerable<WEDOverlay> overlays)
        {
            var tilemaps = new List<WEDTilemap>();
            foreach (var overlay in overlays)
            {
                reader.BaseStream.Seek(overlay.TileOffset, SeekOrigin.Begin);
                tilemaps.Add(reader.ReadStruct<WEDTilemap>());

            }
            return tilemaps;
        }

        private IList<WEDWallGroup> ReadWallgroups(BinaryReader reader, WEDSecondDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.WallGroupOffset, SeekOrigin.Begin);
            return reader.ReadStructs<WEDWallGroup>(descriptor.PolygonCount);
        }
    }
}
