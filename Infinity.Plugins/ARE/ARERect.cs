using System.Runtime.InteropServices;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ARERect
    {
        public short left, top, right, bottom;

        public override bool Equals(object obj)
        {
            if (!(obj is ARERect))
            { return base.Equals(obj); }

            var castObj = (ARERect)obj;

            return Equals(castObj.left, left) && Equals(castObj.top, top) &&
                Equals(castObj.right, right) && Equals(castObj.bottom, bottom);
        }
    }
}
