using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ARERegion
    {
        /// <summary>
        /// Name of the region
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] Name;

        /// <summary>
        /// Region type, such as Proximity Trigger, Travel Region, Info Point etc
        /// </summary>
        public short RegionType;

        /// <summary>
        /// The bounding box for the region
        /// </summary>
        public ARERect BoundingBox;

        /// <summary>
        /// Vertice count that makes up the perimeter
        /// </summary>
        public short PerimeterVerticeCount;

        /// <summary>
        /// The first index of the vertice
        /// </summary>
        public int FirstVertexIndex;

        /// <summary>
        /// Currently unknown data
        /// </summary>
        public int UnknownData;

        /// <summary>
        /// Index link to the Cursors.bam file
        /// </summary>
        public int CursorIndex;

        /// <summary>
        /// Reference to the destination area
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] DestinationArea;

        /// <summary>
        /// Name of the referenced destination area
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] DestinationName;

        /// <summary>
        /// Flags for the region, i.e Invisible Trap, Party Required, NPC Trigger etc.
        /// </summary>
        public int RegionFlags;

        /// <summary>
        /// Info link id to the TLK file
        /// </summary>
        public int InformationLinkId;

        /// <summary>
        /// Trap information for the region
        /// </summary>
        public ARETrap Trap;

        /// <summary>
        /// Reference to the key item
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] KeyItem;

        /// <summary>
        /// Reference to the region script
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] RegionScript;

        /// <summary>
        /// An alternative location
        /// </summary>
        public AREPoint AlternativeLocation;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public byte[] UnknownData2;

        /// <summary>
        /// Reference to the dialog file
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] DialogFile;

        public override bool Equals(object obj)
        {
            if(!(obj is ARERegion))
            { return base.Equals(obj); }

            var castObj = (ARERegion) obj;

            return castObj.Name.SameAs(Name) && castObj.RegionType == RegionType
                   && castObj.BoundingBox.Equals(BoundingBox) &&
                   castObj.PerimeterVerticeCount == PerimeterVerticeCount &&
                   castObj.FirstVertexIndex == FirstVertexIndex &&
                   castObj.UnknownData == UnknownData && castObj.CursorIndex == CursorIndex &&
                   castObj.DestinationArea.SameAs(DestinationArea) &&
                   castObj.DestinationName.SameAs(DestinationName) &&
                   castObj.RegionFlags == RegionFlags && castObj.InformationLinkId == InformationLinkId &&
                   castObj.Trap.Equals(Trap) &&castObj.KeyItem.SameAs(KeyItem) && 
                   castObj.RegionScript.SameAs(RegionScript) &&
                   castObj.AlternativeLocation.Equals(AlternativeLocation) &&
                   castObj.UnknownData2.SameAs(UnknownData2) && castObj.DialogFile.SameAs(DialogFile);
        }
    }
}
