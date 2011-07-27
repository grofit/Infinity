using System.Runtime.InteropServices;

namespace Infinity.Plugins.TIS
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TISDescriptor
    {
        /// <summary>
        /// The amount of tiles contained within the tileset
        /// </summary>
        public int TileCount;

        /// <summary>
        /// The total size in bytes of all the tile resources
        /// </summary>
        public int TilesSize;

        /// <summary>
        /// The offset to the tile data, from the start of the stream
        /// </summary>
        public int TileOffset;

        /// <summary>
        /// The dimensions of the tiles contained (used for both x/y)
        /// </summary>
        public int TileDimensions;
    }
}
