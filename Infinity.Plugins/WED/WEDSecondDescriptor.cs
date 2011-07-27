using System.Runtime.InteropServices;

namespace Infinity.Plugins.WED
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WEDSecondDescriptor
    {
        /// <summary>
        /// Total number of polygons
        /// </summary>
        public int PolygonCount;

        /// <summary>
        /// Offset to the polygon data
        /// </summary>
        public int PolygonOffset;

        /// <summary>
        /// Offset to the vertice data
        /// </summary>
        public int VerticeOffset;

        /// <summary>
        /// Offset to the wall group data
        /// </summary>
        public int WallGroupOffset;

        /// <summary>
        /// Offset to the index data
        /// </summary>
        public int IndiciesOffset;
    }
}
