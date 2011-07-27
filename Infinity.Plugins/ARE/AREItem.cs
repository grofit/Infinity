using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AREItem
    {
        /// <summary>
        /// Reference to the item
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] ItemReference;

        public short ItemExpirationTime;
        public short Quantity1, Quantity2, Quantity3;

        /// <summary>
        /// Item flags, i.e Identified, Unstealable, Stolen etc
        /// </summary>
        public int ItemFlags;

        public override bool Equals(object obj)
        {
            if (!(obj is AREItem))
            { return base.Equals(obj); }

            var castObj = (AREItem)obj;

            return castObj.ItemReference.SameAs(ItemReference) &&
                   castObj.ItemExpirationTime == ItemExpirationTime &&
                   castObj.Quantity1 == Quantity1 && castObj.Quantity2 == Quantity2 &&
                   castObj.Quantity3 == Quantity3 && castObj.ItemFlags == ItemFlags;
        }
    }
}
