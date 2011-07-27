using System.Runtime.InteropServices;

namespace Infinity.Plugins.BIF
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BIFDescriptor
    {
        /// <summary>
        /// Amount of resources/files contained within Bif file
        /// </summary>
        public int FileCount;
        
        /// <summary>
        /// Amount of tileset files contained within Bif file
        /// </summary>
        public int TilesetCount;

        /// <summary>
        /// Offset to the file entries from the start of the stream
        /// </summary>
        public int FileEntryOffset;

        /// <summary>
        /// Offset to the tileset entries from the start of the stream
        /// </summary>
        public int TilesetEntryOffset;

        public override bool Equals(object obj)
        {
            if(!(obj is BIFDescriptor))
            { return base.Equals(obj); }

            var castObj = (BIFDescriptor) obj;
            return (FileCount.Equals(castObj.FileCount) &&
                    TilesetCount.Equals(castObj.TilesetCount) &&
                    FileEntryOffset.Equals(castObj.FileEntryOffset) &&
                    TilesetEntryOffset.Equals(castObj.TilesetEntryOffset));
        }
    }
}
