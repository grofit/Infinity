using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct AREAnimation
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
        public char[] Name;

        public AREPoint Location;

        public int AppearanceSchedule;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
        public char[] AnimationReference;

        /// <summary>
        /// Reference to the BAM resource sequence number
        /// </summary>
        public short SequenceNumber;

        /// <summary>
        /// Reference to the BAM resource frame number
        /// </summary>
        public short FrameNumber;

        public int AnimationFlags;

        public short Height;
        public short Transparency;
        public short StartingFrame;

        /// <summary>
        /// The chance that the animation will loop, usually 100
        /// </summary>
        public byte LoopingAnimationChance;

        public byte SkipCycles;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] PaletteReference;

        public int UnknownData1;

        public override bool Equals(object obj)
        {
            if (!(obj is AREAnimation))
            { return base.Equals(obj); }

            var castObj = (AREAnimation) obj;

            return castObj.Name.SameAs(Name) && castObj.Location.Equals(Location) &&
                   castObj.AppearanceSchedule == AppearanceSchedule &&
                   castObj.AnimationReference.SameAs(AnimationReference) &&
                   castObj.SequenceNumber == SequenceNumber && castObj.FrameNumber == FrameNumber &&
                   castObj.AnimationFlags == AnimationFlags &&
                   castObj.Height == Height && castObj.Transparency == Transparency &&
                   castObj.StartingFrame == StartingFrame && LoopingAnimationChance == LoopingAnimationChance &&
                   castObj.SkipCycles == SkipCycles && castObj.PaletteReference.SameAs(PaletteReference) &&
                   castObj.UnknownData1 == UnknownData1;

        }
    }
}
