using System.Runtime.InteropServices;

namespace Infinity.Plugins.WED
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class WEDVertice
    {
        /// <summary>
        /// X coordinate for the vertice
        /// </summary>
        public short X { get; set; }

        /// <summary>
        /// Y coordinate for the vertice
        /// </summary>
        public short Y { get; set; }
    }
}
