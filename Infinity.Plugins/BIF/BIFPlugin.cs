using System;
using System.Collections.Generic;
using System.IO;
using Infinity.Configuration;
using Infinity.Lookups;
using netextender.extensions;

namespace Infinity.Plugins.BIF
{
    public class BIFPlugin : IPlugin
    {
        public int PluginSignature
        {
            get { return ResourceTypes.BIF; }
        }

        public string TargetVersion
        {
            get { return "1.0"; }
        }

        private static readonly string SignatureBIF = "BIFFV1  ";
        private static readonly int FileEntrySize = sizeof (int) * 4;
        private static readonly int TileEntrySize = sizeof (int) * 5;

        public BIFResource Import(Stream stream)
        {
            var binaryReader = new BinaryReader(stream);
            
            var signature = ConvertToSignature(binaryReader.ReadBytes(SignatureBIF.Length));
            if (!signature.Equals(SignatureBIF, StringComparison.CurrentCultureIgnoreCase))
            { LoggingConfiguration.LogAndThrowError(new Exception("Invalid signature for BIF file")); }

            var descriptor = ReadDescriptor(binaryReader, (int)binaryReader.BaseStream.Position);

            var bifResource = new BIFResource(
                    ReadFileEntries(binaryReader, descriptor),
                    ReadTileEntries(binaryReader, descriptor)
                );

            return bifResource;
        }

        private string ConvertToSignature(byte[] bytes)
        { return bytes.AsString(); }

        private BIFDescriptor ReadDescriptor(BinaryReader reader, int descriptorOffset)
        {
            reader.BaseStream.Seek(descriptorOffset, SeekOrigin.Begin);
            
            var descriptor = new BIFDescriptor();

            descriptor.FileCount = reader.ReadInt32();
            descriptor.TilesetCount = reader.ReadInt32();
            descriptor.FileEntryOffset = reader.ReadInt32();
            descriptor.TilesetEntryOffset = descriptor.FileEntryOffset + (descriptor.FileCount * FileEntrySize);

            return descriptor;
        }

        private IList<BIFFileEntry> ReadFileEntries(BinaryReader binaryReader, BIFDescriptor bifDescriptor)
        {
            binaryReader.BaseStream.Seek(bifDescriptor.FileEntryOffset, SeekOrigin.Begin);
            return binaryReader.ReadStructs<BIFFileEntry>(bifDescriptor.FileCount);
        }

        private IList<BIFTilesetEntry> ReadTileEntries(BinaryReader binaryReader, BIFDescriptor bifDescriptor)
        {
            binaryReader.BaseStream.Seek(bifDescriptor.TilesetEntryOffset, SeekOrigin.Begin);
            return binaryReader.ReadStructs<BIFTilesetEntry>(bifDescriptor.TilesetCount);
        }
    }
}
