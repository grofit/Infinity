using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ARETiledObject
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] UnknownData1;
        public int UnknownData2;

        public int OpenSearchSquareOffset;
        public int OpenSearchSquareCount;

        public int ClosedSearchSquareOffset;
        public int ClosedSearchSquareCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] UnknownData3;

        public override bool Equals(object obj)
        {
            if (!(obj is ARETiledObject))
            { return base.Equals(obj); }

            var castObj = (ARETiledObject)obj;

            return castObj.Name.SameAs(Name) && castObj.UnknownData1.SameAs(UnknownData1) &&
                   castObj.UnknownData2 == UnknownData2 &&
                   castObj.OpenSearchSquareOffset == OpenSearchSquareOffset &&
                   castObj.OpenSearchSquareCount == OpenSearchSquareCount &&
                   castObj.ClosedSearchSquareOffset == ClosedSearchSquareOffset &&
                   castObj.ClosedSearchSquareCount == ClosedSearchSquareCount &&
                   castObj.UnknownData3.SameAs(UnknownData3);
        }
    }
}
