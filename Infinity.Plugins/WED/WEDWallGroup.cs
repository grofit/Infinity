using System.Runtime.InteropServices;

namespace Infinity.Plugins.WED
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WEDWallGroup
    {
        /// <summary>
        /// Starting index for the polygon
        /// </summary>
        public short StartPolygonIndex { get; set; }

        /// <summary>
        /// Total number of polygons indexed
        /// </summary>
        public short PolygonIndexCount { get; set; }
    }
}
