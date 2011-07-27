using System.Runtime.InteropServices;

namespace Infinity.Plugins.WED
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WEDDescriptor
    {
        /// <summary>
        /// Number of overlays, including the base layer
        /// </summary>
        public int OverlayCount;

        /// <summary>
        /// Number of doors within this section
        /// </summary>
        public int DoorCount;

        /// <summary>
        /// Offset to the overlays, from the start of the stream
        /// </summary>
        public int OverlayOffset;
        
        /// <summary>
        /// Offset to the secondary descriptor
        /// </summary>
        public int SecondaryDescriptorOffset;

        /// <summary>
        /// Offset to the doors, from the start of the stream
        /// </summary>
        public int DoorOffset;

        /// <summary>
        /// Offset to the door tiles, from the start of the stream
        /// </summary>
        public int DoorTileOffset;


        public override bool Equals(object obj)
        {
            if (!(obj is WEDDescriptor))
            { return base.Equals(obj); }

            var castObj = (WEDDescriptor)obj;
            return (OverlayCount.Equals(castObj.OverlayCount) &&
                    DoorCount.Equals(castObj.DoorCount) &&
                    OverlayOffset.Equals(castObj.OverlayOffset) &&
                    DoorOffset.Equals(castObj.DoorOffset) &&
                    DoorTileOffset.Equals(castObj.DoorTileOffset) &&
                    SecondaryDescriptorOffset.Equals(castObj.SecondaryDescriptorOffset));
        }
    }
}
