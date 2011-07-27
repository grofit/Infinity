using System.Runtime.InteropServices;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AREPoint
    {
        public short X;
        public short Y;
    }
}
