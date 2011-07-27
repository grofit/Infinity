using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AREEntrance
    {
        /// <summary>
        /// Name of the entrance
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] Name;

        public AREPoint Location;
        public short Orientation;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 66)]
        public byte[] UnknownData1;

        public override bool Equals(object obj)
        {
            if (!(obj is AREEntrance))
            { return base.Equals(obj); }

            var castObj = (AREEntrance)obj;

            return castObj.Name.SameAs(Name) && castObj.Location.Equals(Location) &&
                   castObj.Orientation == Orientation && castObj.UnknownData1.SameAs(UnknownData1);
        }
    }
}
