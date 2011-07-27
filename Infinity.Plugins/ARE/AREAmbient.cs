using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct AREAmbient
    {
        /// <summary>
        /// Name of the ambient effect
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
        public char[] Name;

        public AREPoint Location;

        public short Radius;
        public short Height;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] UnknownData1;

        public short Volume;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] SoundReference1, SoundReference2, SoundReference3,
                      SoundReference4, SoundReference5, SoundReference6,
                      SoundReference7, SoundReference8, SoundReference9,
                      SoundReference10;

        public short SoundCount;
        public short UnknownData2;

        public int TimeIntervalInSeconds;
        public int TimeDeviation;

        /// <summary>
        /// Schedule for the sound to appear, each bit represents a 1 hour period
        /// </summary>
        public int AppearenceSchedule;

        /// <summary>
        /// Flags for the ambient effect, i.e Enabled, Environmental Effect, Local Effect etc
        /// </summary>
        public int AmbientFlags;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] UnknownData3;

        public override bool Equals(object obj)
        {
            if (!(obj is AREAmbient))
            { return base.Equals(obj); }

            var castObj = (AREAmbient)obj;

            return castObj.Name.SameAs(Name) && castObj.Location.Equals(Location) &&
                   castObj.Radius == Radius && castObj.Height == Height &&
                   castObj.UnknownData1.SameAs(UnknownData1) && castObj.Volume == Volume &&
                   castObj.SoundReference1.SameAs(SoundReference1) &&
                   castObj.SoundReference2.SameAs(SoundReference2) &&
                   castObj.SoundReference3.SameAs(SoundReference3) &&
                   castObj.SoundReference4.SameAs(SoundReference4) &&
                   castObj.SoundReference5.SameAs(SoundReference5) &&
                   castObj.SoundReference6.SameAs(SoundReference6) &&
                   castObj.SoundReference7.SameAs(SoundReference7) &&
                   castObj.SoundReference8.SameAs(SoundReference8) &&
                   castObj.SoundReference9.SameAs(SoundReference9) &&
                   castObj.SoundReference10.SameAs(SoundReference10) &&
                   castObj.SoundCount == SoundCount && castObj.TimeIntervalInSeconds == TimeIntervalInSeconds &&
                   castObj.TimeDeviation == TimeDeviation && castObj.UnknownData2 == UnknownData2 &&
                   castObj.AppearenceSchedule == AppearenceSchedule && castObj.AmbientFlags == AmbientFlags &&
                   castObj.UnknownData3.SameAs(UnknownData3);
        }
    }
}
