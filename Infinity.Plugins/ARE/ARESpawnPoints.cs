using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ARESpawnPoint
    {
        /// <summary>
        /// Name of the spawn point
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] Name;

        /// <summary>
        /// Location of the spawn point
        /// </summary>
        public AREPoint Location;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] CreatureReference1, CreatureReference2, CreatureReference3,
                      CreatureReference4, CreatureReference5, CreatureReference6,
                      CreatureReference7, CreatureReference8, CreatureReference9,
                      CreatureReference10;

        public short CreatureCount;
        public short BaseCreatureNumber;
        public short SecondsBetweenSpawn;

        /// <summary>
        /// Spawn type indicates how it spawns, i.e onRest/onRevealed
        /// </summary>
        public short SpawnType;

        public int UnknownData1;
        public int UnknownData2;

        public short MaximumCreatureCount;
        public short IsEnabled;

        /// <summary>
        /// Schedule for appearance, bits 0-23 represent an hour of game time.
        /// bit0  = 00:30 to 01:29
        /// ..
        /// bit23 = 23:30 to 00:29
        /// </summary>
        public int AppearanceSchedule;

        public short ProbabilityDay, ProbabilityNight;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public byte[] UnknownData3;

        public override bool Equals(object obj)
        {
            if (!(obj is ARESpawnPoint))
            { return base.Equals(obj); }

            var castObj = (ARESpawnPoint)obj;

            return castObj.Name.SameAs(Name) && castObj.Location.Equals(Location) &&
                   castObj.CreatureReference1.SameAs(CreatureReference1) &&
                   castObj.CreatureReference2.SameAs(CreatureReference2) &&
                   castObj.CreatureReference3.SameAs(CreatureReference3) &&
                   castObj.CreatureReference4.SameAs(CreatureReference4) &&
                   castObj.CreatureReference5.SameAs(CreatureReference5) &&
                   castObj.CreatureReference6.SameAs(CreatureReference6) &&
                   castObj.CreatureReference7.SameAs(CreatureReference7) &&
                   castObj.CreatureReference8.SameAs(CreatureReference8) &&
                   castObj.CreatureReference9.SameAs(CreatureReference9) &&
                   castObj.CreatureReference10.SameAs(CreatureReference10) &&
                   castObj.CreatureCount == CreatureCount && castObj.BaseCreatureNumber == BaseCreatureNumber &&
                   castObj.SecondsBetweenSpawn == SecondsBetweenSpawn && castObj.SpawnType == SpawnType &&
                   castObj.UnknownData1 == UnknownData1 && castObj.UnknownData2 == UnknownData2 &&
                   castObj.MaximumCreatureCount == MaximumCreatureCount &&
                   castObj.IsEnabled == IsEnabled && castObj.AppearanceSchedule == AppearanceSchedule &&
                   castObj.ProbabilityDay == ProbabilityDay && castObj.ProbabilityNight == ProbabilityNight &&
                   castObj.UnknownData3.SameAs(castObj.UnknownData3);
        }
    }
}
