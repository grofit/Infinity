using System;
using System.Collections.Generic;
using System.IO;
using Infinity.Configuration;
using Infinity.Lookups;
using netextender.extensions;

namespace Infinity.Plugins.TIS
{
    public class TISPlugin : IPlugin
    {
        public int PluginSignature
        {
            get { return ResourceTypes.TIS; }
        }

        public string TargetVersion
        {
            get { return "1.0"; }
        }
        
        private static readonly string SignatureTIS = "TIS V1  ";

        public IList<TISTile> Import(Stream stream)
        {
            var binaryReader = new BinaryReader(stream);

            var signature = ConvertToSignature(binaryReader.ReadBytes(SignatureTIS.Length));
            if (!signature.Equals(SignatureTIS, StringComparison.CurrentCultureIgnoreCase))
            { LoggingConfiguration.LogAndThrowError(new Exception("Invalid signature for TIS file")); }

            var descriptor = ReadDescriptor(binaryReader);

            return ReadTiles(binaryReader, descriptor);
        }

        private TISDescriptor ReadDescriptor(BinaryReader reader)
        { return reader.ReadStruct<TISDescriptor>(); }

        private string ConvertToSignature(byte[] bytes)
        { return bytes.AsString(); }

        private IList<TISTile> ReadTiles(BinaryReader reader, TISDescriptor descriptor)
        {
            var tileList = new List<TISTile>();
            for(int i=0;i<descriptor.TileCount;i++)
            { tileList.Add(ReadTile(reader, descriptor)); }
            return tileList;
        }

        private TISTile ReadTile(BinaryReader reader, TISDescriptor descriptor)
        {
            var palette = reader.ReadStructs<TISColour>(256);
            var pixels = reader.ReadBytes(descriptor.TileDimensions * 2);

            return new TISTile(palette, pixels, descriptor.TileDimensions);
        }

        /*
        private Image GenerateImage(IList<TISColour> palette, IList<byte> pixels, TISDescriptor descriptor)
        {
            var newBitmap = new Bitmap(descriptor.TileDimensions, descriptor.TileDimensions);
            
            for(int i=0;i<palette.Count;i++)
            { newBitmap.Palette.Entries[i] = Color.FromArgb(palette[i].A, palette[i].R, palette[i].G, palette[i].B); }

            var bitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Size.Width, newBitmap.Size.Width), 
                                                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            var indexedPixels = new byte[pixels.Count];
            System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, indexedPixels, 0, indexedPixels.Length);

            for(int i=0;i<indexedPixels.Length;i++)
            { indexedPixels[i] = pixels[i]; }

            newBitmap.UnlockBits(bitmapData);

            return newBitmap;
        }
        */
    }
}
