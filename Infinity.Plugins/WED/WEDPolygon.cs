using System.Runtime.InteropServices;

namespace Infinity.Plugins.WED
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WEDPolygon
    {
        /// <summary>
        /// The starting index for the vertex
        /// </summary>
        public int StartingVertexIndex;

        /// <summary>
        /// The total number of vertex
        /// </summary>
        public int VertexCount;

        /// <summary>
        /// The state type for the polygon, such as a shaded wall, door, cover animation etc
        /// </summary>
        public byte StateType;

        /// <summary>
        /// Currently unknown data
        /// </summary>
        public byte UnknownData;

        /// <summary>
        /// Minimum X coordinates of the bounding box
        /// </summary>
        public short MinXBounds;

        /// <summary>
        /// Maximum X coordinates of the bounding box
        /// </summary>
        public short MaxXBounds;

        /// <summary>
        /// Minimum Y coordinates of the bounding box
        /// </summary>
        public short MinYBounds;

        /// <summary>
        /// Maximum Y coordinates of the bounding box
        /// </summary>
        public short MaxYBounds;
    }
}
