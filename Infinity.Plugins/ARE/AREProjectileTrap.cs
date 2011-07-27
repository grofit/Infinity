using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AREProjectileTrap
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] ProjectileReference;

        public int EffectBlockOffset;
        public short EffectBlockCount;

        /// <summary>
        /// Reference to the index within projectl.ids
        /// </summary>
        public short MissileReference;

        public int UnknownData1;
       
        public AREPoint Location;

        public short UnknownData2;
       
        public short CasterId;

        public override bool Equals(object obj)
        {
            if (!(obj is AREProjectileTrap))
            { return base.Equals(obj); }

            var castObj = (AREProjectileTrap)obj;

            return castObj.ProjectileReference.SameAs(ProjectileReference) &&
                   castObj.EffectBlockOffset == EffectBlockOffset &&
                   castObj.EffectBlockCount == EffectBlockCount &&
                   castObj.MissileReference == MissileReference &&
                   castObj.UnknownData1 == UnknownData1 &&
                   castObj.Location.Equals(Location) && castObj.UnknownData2 == UnknownData2 &&
                   castObj.CasterId == CasterId;
        }
    }
}
