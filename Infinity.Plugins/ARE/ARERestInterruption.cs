using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ARERestInterruption
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] InterruptionExplanationText;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] CreatureSpawnReference1, CreatureSpawnReference2, CreatureSpawnReference3,
                      CreatureSpawnReference4, CreatureSpawnReference5, CreatureSpawnReference6,
                      CreatureSpawnReference7, CreatureSpawnReference8, CreatureSpawnReference9,
                      CreatureSpawnReference10;

        public short CreatureSpawnCount;

        public short CreatureSpawnFrequency;

        public int UnknownData1;
        public int UnknownData2;

        public short MaximumCreatureSpawnCount;

        public short UnknownData3;

        public short ProbabilityDay;
        public short ProbabilityNight;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public byte[] UnknownData4;

        public override bool Equals(object obj)
        {
            if (!(obj is ARERestInterruption))
            { return base.Equals(obj); }

            var castObj = (ARERestInterruption)obj;

            return castObj.Name.SameAs(Name) && castObj.InterruptionExplanationText.SameAs(InterruptionExplanationText) &&
                   castObj.CreatureSpawnReference1.SameAs(CreatureSpawnReference1) &&
                   castObj.CreatureSpawnReference2.SameAs(CreatureSpawnReference2) &&
                   castObj.CreatureSpawnReference3.SameAs(CreatureSpawnReference3) &&
                   castObj.CreatureSpawnReference4.SameAs(CreatureSpawnReference4) &&
                   castObj.CreatureSpawnReference5.SameAs(CreatureSpawnReference5) &&
                   castObj.CreatureSpawnReference6.SameAs(CreatureSpawnReference6) &&
                   castObj.CreatureSpawnReference7.SameAs(CreatureSpawnReference7) &&
                   castObj.CreatureSpawnReference8.SameAs(CreatureSpawnReference8) &&
                   castObj.CreatureSpawnReference9.SameAs(CreatureSpawnReference9) &&
                   castObj.CreatureSpawnReference10.SameAs(CreatureSpawnReference10) &&
                   castObj.CreatureSpawnCount == CreatureSpawnCount &&
                   castObj.CreatureSpawnFrequency == CreatureSpawnFrequency &&
                   castObj.UnknownData1 == UnknownData1 &&
                   castObj.UnknownData2 == UnknownData2 &&
                   castObj.MaximumCreatureSpawnCount == MaximumCreatureSpawnCount &&
                   castObj.UnknownData3 == UnknownData3 &&
                   castObj.ProbabilityDay == ProbabilityDay &&
                   castObj.ProbabilityNight == ProbabilityNight &&
                   castObj.UnknownData4.SameAs(UnknownData4);

        }
    }
}
