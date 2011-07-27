using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct AREVariable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
        public char[] Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] UnknownData1;
        
        public int Value;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public byte[] UnknownData2;

        public override bool Equals(object obj)
        {
            if (!(obj is AREVariable))
            { return base.Equals(obj); }

            var castObj = (AREVariable)obj;

            return castObj.Name.SameAs(Name) && castObj.UnknownData1.SameAs(UnknownData1) &&
                   castObj.Value == Value && castObj.UnknownData2.SameAs(UnknownData2);
        }
    }
}
