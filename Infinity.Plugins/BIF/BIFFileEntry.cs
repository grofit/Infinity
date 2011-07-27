using System.Runtime.InteropServices;

namespace Infinity.Plugins.BIF
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BIFFileEntry
    {
        /// <summary>
        /// Resource Locator, matched against .key file resource entries
        /// </summary>
        public int Locator;

        /// <summary>
        /// Offset to resource data, from the start of the stream
        /// </summary>
        public int Offset;

        /// <summary>
        /// The size of the resource data (in bytes)
        /// </summary>
        public int Size;

        /// <summary>
        /// The type of resource that the entry references (ResourceTypes)
        /// </summary>
        public short Type;

        /// <summary>
        /// Currently unknown data
        /// </summary>
        public short UnknownData;

        public BIFFileEntry(int locator, int offset, int size, short type, short unknownData)
        {
            Locator = locator;
            Offset = offset;
            Size = size;
            Type = type;
            UnknownData = unknownData;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BIFTilesetEntry))
            { return base.Equals(obj); }

            var castObj = (BIFTilesetEntry)obj;

            return (Locator.Equals(castObj.Locator) &&
                    Offset.Equals(castObj.Offset) &&
                    Size.Equals(castObj.Size) &&
                    Type.Equals(castObj.Type) &&
                    UnknownData.Equals(castObj.UnknownData));
        }
    }
}
