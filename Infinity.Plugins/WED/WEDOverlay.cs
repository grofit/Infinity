using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.WED
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WEDOverlay
    {
        /// <summary>
        /// The width of the overlay
        /// </summary>
        public short Width;

        /// <summary>
        /// The height of the overlay
        /// </summary>
        public short Height;

        /// <summary>
        /// The name of the resource type, usually 8 characters
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] ResourceType;

        /// <summary>
        /// Currently unknown data
        /// </summary>
        public int UnknownData;

        /// <summary>
        /// The offset to the overlays tiles
        /// </summary>
        public int TileOffset;

        /// <summary>
        /// The offset to the tile indexes
        /// </summary>
        public int TileIndexOffset;

        public override bool Equals(object obj)
        {
            if(!(obj is WEDOverlay))
            { return base.Equals(obj); }

            var castObj = (WEDOverlay)obj;
            return (Width.Equals(castObj.Width) &&
                    Height.Equals(castObj.Height) &&
                    ResourceType.SameAs(castObj.ResourceType) &&
                    UnknownData.Equals(castObj.UnknownData) &&
                    TileOffset.Equals(castObj.TileOffset) &&
                    TileIndexOffset.Equals(castObj.TileIndexOffset));
        }
    }
}
