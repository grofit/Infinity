using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AREDoor
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] Name;

        /// <summary>
        /// Door link to the WED resource
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] DoorLinkId;

        /// <summary>
        /// Flags for the door, such as Open, Locked, Trap etc
        /// </summary>
        public int DoorFlags;

        public int OpenVertexIndex;
        public int OpenVertexCount;

        public int ClosedVertexCount;
        public int ClosedVertexIndex;

        public ARERect OpenBoundingBox;
        public ARERect ClosedBoundingBox;

        /// <summary>
        /// Index of the first coordinates in the WED tilemap structure
        /// </summary>
        public int ClosedCellBlockIndex;
        public int ClosedCellBlockCount;

        public int OpenCellBlockCount;
        public int OpenCellBlockIndex;

        public int UnknownData1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] DoorOpenSoundReference;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] DoorClosedSoundReference;

        /// <summary>
        /// Index into the cursors.bam file
        /// </summary>
        public int CursorIndex;

        public ARETrap Trap;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] KeyItem;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] DoorScript;

        public int DetectionDifficulty;
        public int LockDifficulty;

        /// <summary>
        /// Bounding box to specify where the states can be toggled,
        /// can be read as 2 seperate points for each side of door.
        /// </summary>
        public ARERect ToggleStateBoundingBox;

        public int LockpickStringId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] RegionLink;

        public int DialogNameId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] DialogReference;

        public override bool Equals(object obj)
        {
            if (!(obj is AREDoor))
            { return base.Equals(obj); }

            var castObj = (AREDoor)obj;

            return castObj.Name.SameAs(Name) && castObj.DoorLinkId.SameAs(DoorLinkId) &&
                   castObj.DoorFlags == DoorFlags && castObj.OpenVertexIndex == OpenVertexIndex &&
                   castObj.OpenVertexCount == OpenVertexCount && castObj.ClosedVertexCount == ClosedVertexCount &&
                   castObj.ClosedVertexIndex == ClosedVertexIndex && castObj.OpenBoundingBox.Equals(OpenBoundingBox) &&
                   castObj.ClosedBoundingBox.Equals(ClosedBoundingBox) &&
                   castObj.OpenCellBlockIndex == OpenCellBlockIndex &&
                   castObj.OpenCellBlockCount == OpenCellBlockCount &&
                   castObj.ClosedCellBlockCount == ClosedCellBlockCount &&
                   castObj.ClosedCellBlockIndex == ClosedCellBlockIndex && castObj.UnknownData1 == UnknownData1 &&
                   castObj.DoorOpenSoundReference.SameAs(DoorOpenSoundReference) &&
                   castObj.DoorClosedSoundReference.SameAs(DoorClosedSoundReference) &&
                   castObj.CursorIndex == CursorIndex && castObj.Trap.Equals(Trap) &&
                   castObj.KeyItem.SameAs(KeyItem) && castObj.DoorScript.SameAs(DoorScript) &&
                   castObj.DetectionDifficulty == DetectionDifficulty && castObj.LockDifficulty == LockDifficulty &&
                   castObj.ToggleStateBoundingBox.Equals(ToggleStateBoundingBox) &&
                   castObj.LockpickStringId == LockpickStringId && castObj.RegionLink.SameAs(RegionLink) &&
                   castObj.DialogNameId == DialogNameId && castObj.DialogReference.SameAs(DialogReference);
        }
    }
}
