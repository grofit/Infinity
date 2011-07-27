using System.Runtime.InteropServices;

namespace Infinity.Plugins.TIS
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TISColour
    {
        /// <summary>
        /// The RGBA components for the colour
        /// </summary>
        public byte B, G, R, A;
    }
}
