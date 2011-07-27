using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AREActor
    {
        /// <summary>
        /// Name of the actor
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] Name;

        public short CurrentX;
        public short CurrentY;
        public short DestinationX;
        public short DestinationY;

        /// <summary>
        /// Flags assigned to the actor, i.e CRE Attached, Override Script Name
        /// </summary>
        public int ActorFlags;

        /// <summary>
        /// Has actor spawned
        /// </summary>
        public int IsSpawned;

        /// <summary>
        /// Animation for the actor
        /// </summary>
        public int Animation;

        /// <summary>
        /// Orientation for the actor
        /// </summary>
        public int Orientation;

        /// <summary>
        /// Removal time in absolute ticks, -1 to avoid removal
        /// </summary>
        public int RemovalTime;

        /// <summary>
        /// Currently unknown data
        /// </summary>
        public int UnknownData;

        /// <summary>
        /// Schedule for actor appearance, bits 0-23 represent an hour of game time.
        /// bit0  = 00:30 to 01:29
        /// ..
        /// bit23 = 23:30 to 00:29
        /// </summary>
        public int AppearanceSchedule;

        /// <summary>
        /// Number of times the player has talked to actor
        /// </summary>
        public int TimesTalkedTo;

        /// <summary>
        /// Dialog resource reference
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Dialog;

        /// <summary>
        /// Override script reference for this actor
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] OverrideScript;

        /// <summary>
        /// General script reference for this actor
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] GeneralScript;

        /// <summary>
        /// Class script reference for this actor
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] ClassScript;

        /// <summary>
        /// Race script reference for this actor
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] RaceScript;

        /// <summary>
        /// Default script reference for this actor
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] DefaultScript;

        /// <summary>
        /// Specific script reference for this actor
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] 
        public char[] SpecificScript;
        
        /// <summary>
        /// CRE resource reference
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] CREFile;

        /// <summary>
        /// Offset to the CRE data
        /// </summary>
        public int CREOffset;

        /// <summary>
        /// Size of the CRE data
        /// </summary>
        public int CRESize;

        /// <summary>
        /// Currently unknown data
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] UnknownEndData;

        public override bool Equals(object obj)
        {
            if (!(obj is AREActor))
            { return base.Equals(obj); }

            var castObj = (AREActor)obj;

            return castObj.Name.SameAs(Name) && castObj.CurrentX == CurrentX &&
                castObj.CurrentY == CurrentY && castObj.DestinationX == DestinationX &&
                castObj.DestinationY == DestinationY && castObj.ActorFlags == ActorFlags &&
                castObj.IsSpawned == IsSpawned && castObj.Animation == Animation &&
                castObj.RemovalTime == RemovalTime && castObj.UnknownData == UnknownData &&
                castObj.AppearanceSchedule == AppearanceSchedule && castObj.TimesTalkedTo == TimesTalkedTo &&
                castObj.Dialog.SameAs(Dialog) && castObj.OverrideScript.SameAs(OverrideScript) &&
                castObj.GeneralScript.SameAs(GeneralScript) && castObj.ClassScript.SameAs(ClassScript) &&
                castObj.RaceScript.SameAs(RaceScript) && castObj.DefaultScript.SameAs(DefaultScript) &&
                castObj.SpecificScript.SameAs(SpecificScript) && castObj.CREFile.SameAs(CREFile) &&
                castObj.CREOffset == CREOffset && castObj.CRESize == CRESize &&
                castObj.UnknownEndData.SameAs(UnknownEndData);
        }
    }
}
