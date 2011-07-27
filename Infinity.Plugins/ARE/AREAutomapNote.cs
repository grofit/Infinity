using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AREAutomapNote
    {
        public AREPoint Location;

        /// <summary>
        /// Reference to the actual text, will be located either within TOH/TOT files or TLK file
        /// based on the value of IsInternalReference (1=TLK, 0=TOH/TOT)
        /// </summary>
        public int TextReference;

        /// <summary>
        /// Specifies if the reference is to an internal TLK file or an external TOH/TOT file
        /// 0 = TOH/TOT
        /// 1 = TLK
        /// </summary>
        public short IsInternalReference;

        /// <summary>
        /// Colour of automap marker
        /// 
        /// 0 - Gray (default value)
        /// 1 - Violet
        /// 2 - Green
        /// 3 - Orange
        /// 4 - Red
        /// 5 - Blue
        /// 6 - Dark Blue
        /// 7 - Light Gray
        /// </summary>
        public short MarkerColour;

        public int NoteCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst=36)]
        public byte[] UnknownData1;

        public override bool Equals(object obj)
        {
            if (!(obj is AREAutomapNote))
            { return base.Equals(obj); }

            var castObj = (AREAutomapNote)obj;

            return castObj.Location.Equals(Location) &&
                   castObj.TextReference == TextReference &&
                   castObj.IsInternalReference == IsInternalReference &&
                   castObj.MarkerColour == MarkerColour &&
                   castObj.NoteCount == NoteCount &&
                   castObj.UnknownData1.SameAs(UnknownData1);
        }
    }
}
