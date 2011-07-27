using System.Runtime.InteropServices;

namespace Infinity.Plugins.WED
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WEDTilemap
    {
        /// <summary>
        /// Start index in the tile index lookup table
        /// </summary>
        public short TilesetStartIndex;

        /// <summary>
        /// The number of tiles in the lookup table
        /// </summary>
        public short TilesetCount;

        /// <summary>
        /// The index of a second state, i.e closed door state tile
        /// </summary>
        public short SecondaryTilesetIndex;

        /// <summary>
        /// Overlay layer to draw
        /// </summary>
        public int OverlayToDraw;
    }
}
