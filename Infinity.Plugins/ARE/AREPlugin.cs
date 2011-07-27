using System;
using System.Collections.Generic;
using System.IO;
using Infinity.Configuration;
using Infinity.Lookups;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    public class AREPlugin : IPlugin
    {
        public int PluginSignature
        {
            get { return ResourceTypes.ARE; }
        }

        public string TargetVersion
        {
            get { return "1.0"; }
        }

        private static readonly string SignatureARE = "AREAV1.0";

        public void Import(Stream stream)
        {
            var binaryReader = new BinaryReader(stream);

            var signature = ConvertToSignature(binaryReader.ReadBytes(SignatureARE.Length));
            if (!signature.Equals(SignatureARE, StringComparison.CurrentCultureIgnoreCase))
            { LoggingConfiguration.LogAndThrowError(new Exception("Invalid signature for ARE file")); }

            var descriptor = ReadDescriptor(binaryReader, (int)binaryReader.BaseStream.Position);

            var actors = ReadActors(binaryReader, descriptor);
            var regions = ReadRegions(binaryReader, descriptor);
            var spawnPoints = ReadSpawnPoints(binaryReader, descriptor);
            var entrances = ReadEntrances(binaryReader, descriptor);
            var containers = ReadContainers(binaryReader, descriptor);
            var items = ReadItems(binaryReader, descriptor);
            var vertices = ReadVertices(binaryReader, descriptor);
            var ambients = ReadAmbients(binaryReader, descriptor);
            var variables = ReadVariables(binaryReader, descriptor);
            var doors = ReadDoors(binaryReader, descriptor);
            var animations = ReadAnimations(binaryReader, descriptor);
            var automapNotes = ReadAutomapNotes(binaryReader, descriptor);
            var tiledObjects = ReadTiledObjects(binaryReader, descriptor);
            var projectileTraps = ReadProjectileTraps(binaryReader, descriptor);
            var songEntries = ReadSongEntries(binaryReader, descriptor);
            var restInterruptions = ReadRestInterruptions(binaryReader, descriptor);
        }

        private string ConvertToSignature(byte[] bytes)
        { return bytes.AsString(); }

        private AREDescriptor ReadDescriptor(BinaryReader reader, int descriptorOffset)
        {
            reader.BaseStream.Seek(descriptorOffset, SeekOrigin.Begin);
            return reader.ReadStruct<AREDescriptor>();
        }

        private IList<AREActor> ReadActors(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.ActorOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREActor>(descriptor.ActorCount);
        }

        private IList<ARERegion> ReadRegions(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.RegionOffset, SeekOrigin.Begin);
            return reader.ReadStructs<ARERegion>(descriptor.RegionCount);
        }

        private IList<ARESpawnPoint> ReadSpawnPoints(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.SpawnPointOffset, SeekOrigin.Begin);
            return reader.ReadStructs<ARESpawnPoint>(descriptor.SpawnPointCount);
        }

        private IList<AREEntrance> ReadEntrances(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.EntranceOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREEntrance>(descriptor.EntranceCount);
        }

        private IList<AREContainer> ReadContainers(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.ContainerOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREContainer>(descriptor.ContainerCount);
        }

        private IList<AREItem> ReadItems(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.ItemOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREItem>(descriptor.ItemCount);
        }

        private IList<AREPoint> ReadVertices(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.VerticeOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREPoint>(descriptor.VerticeCount);
        }

        private IList<AREAmbient> ReadAmbients(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.AmbientSoundOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREAmbient>(descriptor.AmbientSoundCount);
        }

        private IList<AREVariable> ReadVariables(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.VariableOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREVariable>(descriptor.VariableCount);
        }

        private void ReadExploredBitmask(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.ExploredBitmapOffset, SeekOrigin.Begin);
            throw new Exception("Not implemented correctly yet");   
        }

        private IList<AREDoor> ReadDoors(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.DoorOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREDoor>(descriptor.DoorCount);
        }

        private IList<AREAnimation> ReadAnimations(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.AnimationOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREAnimation>(descriptor.AnimationCount);
        }

        private IList<AREAutomapNote> ReadAutomapNotes(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.AutomapNoteOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREAutomapNote>(descriptor.AutomapNoteCount);
        }

        private IList<ARETiledObject> ReadTiledObjects(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.TiledObjectOffset, SeekOrigin.Begin);
            return reader.ReadStructs<ARETiledObject>(descriptor.TiledObjectCount);
        }

        private IList<AREProjectileTrap> ReadProjectileTraps(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.ProjectileTrapOffset, SeekOrigin.Begin);
            return reader.ReadStructs<AREProjectileTrap>(descriptor.ProjectileTrapCount);
        }

        private IList<ARESongEntry> ReadSongEntries(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.SongEntryOffset, SeekOrigin.Begin);
            return reader.ReadStructs<ARESongEntry>(1);
        }

        private IList<ARERestInterruption> ReadRestInterruptions(BinaryReader reader, AREDescriptor descriptor)
        {
            reader.BaseStream.Seek(descriptor.RestInterruptionOffset, SeekOrigin.Begin);
            return reader.ReadStructs<ARERestInterruption>(1);
        }
    }
}
