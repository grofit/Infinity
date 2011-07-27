using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AREDescriptor
    {
        /// <summary>
        /// Reference to the area's .wed file, usually 8 characters
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] WedResource;

        /// <summary>
        /// The seconds since last save
        /// </summary>
        public int LastSaveTime;

        /// <summary>
        /// Flags related to the area, i.e Save Allowed, Dream Area
        /// </summary>
        public int AreaFlags;

        /// <summary>
        /// Reference to the area's northern link, usually 8 characters
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] NorthLinkResource;

        /// <summary>
        /// Unknown data related to northern link
        /// </summary>
        public int UnknownNorthData;

        /// <summary>
        /// Reference to the area's eastern link, usually 8 characters
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] EastLinkResource;

        /// <summary>
        /// Unknown data related to eastern link
        /// </summary>
        public int UnknownEastData;

        /// <summary>
        /// Reference to the area's southern link, usually 8 characters
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] SouthLinkResource;

        /// <summary>
        /// Unknown data related to southern link
        /// </summary>
        public int UnknownSouthData;

        /// <summary>
        /// Reference to the area's northern link, usually 8 characters
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] WestLinkResource;

        /// <summary>
        /// Unknown data related to western link
        /// </summary>
        public int UnknownWestData;

        /// <summary>
        /// Area type, such as Outdoor, City, Dungeon etc
        /// </summary>
        public short AreaType;
        
        public short RainProbability;
        public short SnowProbability;
        public short FogProbability;
        public short LightningProbability;
        public short UnknownProbability;

        /// <summary>
        /// Offset to the actor data, from the stream start
        /// </summary>
        public int ActorOffset;
        
        /// <summary>
        /// Number of actors within the area
        /// </summary>
        public short ActorCount;

        /// <summary>
        /// Number of regions within the area
        /// </summary>
        public short RegionCount;

        /// <summary>
        /// Offset to the region data
        /// </summary>
        public int RegionOffset;

        /// <summary>
        /// Offset to the spawn point data
        /// </summary>
        public int SpawnPointOffset;

        /// <summary>
        /// Number of spawn points within the area
        /// </summary>
        public int SpawnPointCount;

        /// <summary>
        /// Offset to the entrance data
        /// </summary>
        public int EntranceOffset;

        /// <summary>
        /// Number of entrances within area
        /// </summary>
        public int EntranceCount;

        /// <summary>
        /// Offset to the container data
        /// </summary>
        public int ContainerOffset;

        /// <summary>
        /// Number of containers within the area
        /// </summary>
        public short ContainerCount;

        /// <summary>
        /// Number of items within the area
        /// </summary>
        public short ItemCount;

        /// <summary>
        /// Offset to item data
        /// </summary>
        public int ItemOffset;

        /// <summary>
        /// Offset to the vertice data
        /// </summary>
        public int VerticeOffset;

        /// <summary>
        /// Number of vertices within the resource
        /// </summary>
        public short VerticeCount;

        /// <summary>
        /// Number of ambient sounds within the area
        /// </summary>
        public short AmbientSoundCount;

        /// <summary>
        /// Offset to the ambient sound data
        /// </summary>
        public int AmbientSoundOffset;

        /// <summary>
        /// Offset to the variable data
        /// </summary>
        public int VariableOffset;

        /// <summary>
        /// Number of variables within this resource
        /// </summary>
        public int VariableCount;

        /// <summary>
        /// Currently unknown data
        /// </summary>
        public int UnknownData;

        /// <summary>
        /// Reference to the areas script file
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] AreaScript;

        /// <summary>
        /// Size of the explored bitmap structure
        /// </summary>
        public int ExploredBitmaskSize;

        /// <summary>
        /// Offset to the explored bitmask structure
        /// </summary>
        public int ExploredBitmapOffset;

        /// <summary>
        /// Number of doors within this area
        /// </summary>
        public int DoorCount;

        /// <summary>
        /// Offset to the door data
        /// </summary>
        public int DoorOffset;

        /// <summary>
        /// Number of animations within this area
        /// </summary>
        public int AnimationCount;

        /// <summary>
        /// Offset to the animation data
        /// </summary>
        public int AnimationOffset;

        /// <summary>
        /// Number of tiled objects within this area
        /// </summary>
        public int TiledObjectCount;

        /// <summary>
        /// Offset to the tiled object data
        /// </summary>
        public int TiledObjectOffset;

        /// <summary>
        /// Offset to the song entry data
        /// </summary>
        public int SongEntryOffset;

        /// <summary>
        /// Offset to the rest interruption data
        /// </summary>
        public int RestInterruptionOffset;

        public int AutomapNoteOffset;
        public int AutomapNoteCount;

        public int ProjectileTrapOffset;
        public int ProjectileTrapCount;

        /// <summary>
        /// General Data 3, is used differently in other games
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] GeneralData1;

        /// <summary>
        /// General Data 4, is used differently in other games
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] GeneralData2;

        /// <summary>
        /// Currently unknown data
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 38)]
        public byte[] UnknownEndData;

        public override bool Equals(object obj)
        {
            if (!(obj is AREDescriptor))
            { return base.Equals(obj); }

            var castObj = (AREDescriptor)obj;

            return castObj.WedResource.SameAs(WedResource) && castObj.LastSaveTime == LastSaveTime && 
                castObj.AreaFlags == AreaFlags && castObj.NorthLinkResource.SameAs(NorthLinkResource) && 
                castObj.UnknownNorthData == UnknownNorthData && castObj.EastLinkResource.SameAs(EastLinkResource) && 
                castObj.UnknownEastData == UnknownEastData && castObj.SouthLinkResource.SameAs(SouthLinkResource) && 
                castObj.UnknownSouthData == UnknownSouthData && castObj.WestLinkResource.SameAs(WestLinkResource) && 
                castObj.UnknownWestData == UnknownWestData && castObj.AreaType == AreaType && 
                castObj.RainProbability == RainProbability && castObj.SnowProbability == SnowProbability && 
                castObj.FogProbability == FogProbability && castObj.LightningProbability == LightningProbability && 
                castObj.UnknownProbability == UnknownProbability && castObj.ActorOffset == ActorOffset && 
                castObj.ActorCount == ActorCount && castObj.RegionCount == RegionCount && 
                castObj.RegionOffset == RegionOffset && castObj.SpawnPointOffset == SpawnPointOffset && 
                castObj.SpawnPointCount == SpawnPointCount && castObj.EntranceOffset == EntranceOffset && 
                castObj.EntranceCount == EntranceCount && castObj.ContainerOffset == ContainerOffset && 
                castObj.ContainerCount == ContainerCount && castObj.ItemCount == ItemCount && 
                castObj.ItemOffset == ItemOffset && castObj.VerticeOffset == VerticeOffset && 
                castObj.VerticeCount == VerticeCount && castObj.AmbientSoundCount == AmbientSoundCount && 
                castObj.AmbientSoundOffset == AmbientSoundOffset && castObj.VariableOffset == VariableOffset && 
                castObj.VariableCount == VariableCount && castObj.UnknownData == UnknownData && 
                castObj.AreaScript.SameAs(AreaScript) && castObj.ExploredBitmaskSize == ExploredBitmaskSize && 
                castObj.ExploredBitmapOffset == ExploredBitmapOffset && castObj.DoorCount == DoorCount && 
                castObj.DoorOffset == DoorOffset && castObj.AnimationCount == AnimationCount && 
                castObj.AnimationOffset == AnimationOffset && castObj.TiledObjectCount == TiledObjectCount && 
                castObj.TiledObjectOffset == TiledObjectOffset && castObj.SongEntryOffset == SongEntryOffset && 
                castObj.RestInterruptionOffset == RestInterruptionOffset && castObj.AutomapNoteOffset == AutomapNoteOffset &&
                castObj.AutomapNoteCount == AutomapNoteCount && castObj.GeneralData1.SameAs(GeneralData1) &&
                castObj.ProjectileTrapOffset == ProjectileTrapOffset && castObj.ProjectileTrapCount == ProjectileTrapCount &&
                castObj.GeneralData2.SameAs(GeneralData2);
        }
    }
}