using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AREContainer
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] Name;

        public AREPoint Location;

        /// <summary>
        /// The type of container, i.e Bag, Chest, Drawer, Pile, Body etc
        /// </summary>
        public short ContainerType;

        public short LockDifficulty;

        /// <summary>
        /// The flags of the container, i.e Locked, Trap Reset, Disabled etc
        /// </summary>
        public int ContainerFlags;

        /// <summary>
        /// Trap information for the container
        /// </summary>
        public ARETrap Trap;

        public ARERect MinimumBoundingBox;
        
        /// <summary>
        /// Index of the first item in the container
        /// </summary>
        public int ItemIndex;
        public int ItemCount;

        /// <summary>
        /// Reference to the trap script for this container
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] TrapScript;

        /// <summary>
        /// Index of the first vertex making up the outline of the container
        /// </summary>
        public int VertexIndex;
        public int VertexCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] UnknownData1;

        /// <summary>
        /// Reference to the key item
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] KeyItem;

        public int UnknownData2;

        /// <summary>
        /// Link to the TLK file for the lockpick string resource
        /// </summary>
        public int LockpickLinkId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public byte[] UnknownData3;

        public override bool Equals(object obj)
        {
            if (!(obj is AREContainer))
            { return base.Equals(obj); }

            var castObj = (AREContainer)obj;

            return castObj.Name.SameAs(Name) && castObj.Location.Equals(Location) &&
                   castObj.ContainerType == ContainerType && castObj.LockDifficulty == LockDifficulty &&
                   castObj.ContainerFlags == ContainerFlags && castObj.Trap.Equals(Trap) &&
                   castObj.MinimumBoundingBox.Equals(MinimumBoundingBox) &&
                   castObj.ItemIndex == ItemIndex && castObj.ItemCount == ItemCount &&
                   castObj.TrapScript.SameAs(TrapScript) && castObj.VertexIndex == VertexIndex &&
                   castObj.VertexCount == VertexCount && castObj.UnknownData1.SameAs(UnknownData1) &&
                   castObj.KeyItem.SameAs(KeyItem) && castObj.UnknownData2 == UnknownData2 &&
                   castObj.LockpickLinkId == LockpickLinkId && castObj.UnknownData3.SameAs(UnknownData3);
        }
    }
}
