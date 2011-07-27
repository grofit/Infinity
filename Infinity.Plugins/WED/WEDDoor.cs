using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.WED
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WEDDoor
    {
        /// <summary>
        /// The name of the door, usually 8 characters
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Name;

        /// <summary>
        /// Is the door open
        /// </summary>
        public short IsOpen;

        /// <summary>
        /// The first tile cell index of the door
        /// </summary>
        public short TileCellIndex;

        /// <summary>
        /// Number of tile cells for this door
        /// </summary>
        public short TileCellCount;

        /// <summary>
        /// Number of polygons used when the door is open
        /// </summary>
        public short DoorOpenPolygonCount;

        /// <summary>
        /// Number of polygons used when the doors is closed
        /// </summary>
        public short DoorClosedPolygonCount;

        /// <summary>
        /// The offset to the door open polygons, from the start of the stream
        /// </summary>
        public int DoorOpenPolygonOffset;

        /// <summary>
        /// The offset to the door
        /// </summary>
        public int DoorClosedPolygonOffset;

        public override bool Equals(object obj)
        {
            if (!(obj is WEDDoor))
            { return base.Equals(obj); }

            var castObj = (WEDDoor)obj;
            return (Name.SameAs(castObj.Name) &&
                    IsOpen.Equals(castObj.IsOpen) &&
                    TileCellIndex.Equals(castObj.TileCellIndex) &&
                    TileCellCount.Equals(castObj.TileCellCount) &&
                    DoorOpenPolygonCount.Equals(castObj.DoorOpenPolygonCount) &&
                    DoorClosedPolygonCount.Equals(castObj.DoorClosedPolygonCount) &&
                    DoorOpenPolygonOffset.Equals(castObj.DoorOpenPolygonOffset) &&
                    DoorClosedPolygonOffset.Equals(castObj.DoorClosedPolygonOffset));
        }
    }
}
