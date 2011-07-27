using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Infinity.Plugins.ARE;
using netextender.extensions;
using NUnit.Framework;
using TestCommon.Builders.Plugins.ARE;

namespace UnitTests.Plugins
{
    [TestFixture]
    public class AREPlugin_Tests
    {
        [Test]
        public void should_correctly_read_file_signature()
        {
            var expectedOutput = "AREAV1.0";
            var plugin = new AREPlugin();

            var getSignatureMethod = plugin.GetType().GetMethod("ConvertToSignature", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = getSignatureMethod.Invoke(plugin, new[] { expectedOutput.AsBytes() });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_create_descriptor()
        {
            var expectedOutput = new AREDescriptor();
            expectedOutput.WedResource = "example ".ToCharArray();
            expectedOutput.LastSaveTime = 0;
            expectedOutput.AreaFlags = 0x1;
            expectedOutput.NorthLinkResource = "nortreso".ToCharArray();
            expectedOutput.EastLinkResource = "eastreso".ToCharArray();
            expectedOutput.SouthLinkResource = "soutreso".ToCharArray();
            expectedOutput.WestLinkResource = "westreso".ToCharArray();
            expectedOutput.AreaType = 0x3;
            expectedOutput.RainProbability = 125;
            expectedOutput.SnowProbability = 25;
            expectedOutput.FogProbability = 742;
            expectedOutput.LightningProbability = 32;
            expectedOutput.ActorOffset = 0x1C;
            expectedOutput.ActorCount = 10;
            expectedOutput.RegionOffset = 0x25;
            expectedOutput.RegionCount = 5;
            expectedOutput.SpawnPointOffset = 0X3A;
            expectedOutput.SpawnPointCount = 2;
            expectedOutput.EntranceOffset = 0x40;
            expectedOutput.EntranceCount = 1;
            expectedOutput.ContainerOffset = 0x5A;
            expectedOutput.ContainerCount = 120;
            expectedOutput.ItemOffset = 0x6F;
            expectedOutput.ItemCount = 94;
            expectedOutput.VerticeOffset = 0x7F;
            expectedOutput.VerticeCount = 1000;
            expectedOutput.AmbientSoundOffset = 0x82;
            expectedOutput.AmbientSoundCount = 1;
            expectedOutput.VariableOffset = 0x91;
            expectedOutput.VariableCount = 12;
            expectedOutput.AreaScript = "areascri".ToCharArray();
            expectedOutput.ExploredBitmapOffset = 0xAA;
            expectedOutput.ExploredBitmaskSize = 12;
            expectedOutput.DoorOffset = 0xBA;
            expectedOutput.DoorCount = 20;
            expectedOutput.AnimationOffset = 0xC1;
            expectedOutput.AnimationCount = 38;
            expectedOutput.TiledObjectOffset = 0xD0;
            expectedOutput.TiledObjectCount = 10;
            expectedOutput.SongEntryOffset = 0xED;
            expectedOutput.RestInterruptionOffset = 0xFF;
            expectedOutput.GeneralData1 = new char[8];
            expectedOutput.GeneralData2 = new char[8];
            expectedOutput.UnknownEndData = new byte[38];

            var plugin = new AREPlugin();

            var memoryStream = new MemoryStream();
            memoryStream.WriteStruct(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadDescriptor", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, 0 });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_create_actors()
        {
            var actorBuilder = new AREActorBuilder();

            var expectedOutput = new List<AREActor>
                                     {
                                         actorBuilder.WithName("Example Actor 1".ToCharArray()).Build(),
                                         actorBuilder.WithName("Example Actor 2".ToCharArray()).Build()
                                     };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.ActorCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadActors", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_create_regions()
        {
            var region1 = new ARERegion();
            region1.Name = "example region 1".PadRight(32).ToCharArray();
            region1.BoundingBox = new ARERect();
            region1.BoundingBox.top = 100;
            region1.BoundingBox.left = 100;
            region1.BoundingBox.right = 100;
            region1.BoundingBox.bottom = 100;
            region1.FirstVertexIndex = 0;
            region1.PerimeterVerticeCount = 10;
            region1.RegionType = 0x1;
            region1.UnknownData = 0;
            region1.CursorIndex = 1;
            region1.DestinationArea = "areareso".ToCharArray();
            region1.DestinationName = "example region 2".PadRight(32).ToCharArray();
            region1.RegionFlags = 0x1;
            region1.InformationLinkId = 0xFCDA32;
            region1.Trap = new ARETrap()
                               {
                                   TrapDetectionDifficulty = 32,
                                   TrapRemovalDifficulty = 71,
                                   IsTrapped = 1,
                                   IsTrapDetected = 0,
                                   TrapLocation = new AREPoint() {X = 100, Y = 100}
                               };
            region1.KeyItem = "keyitem1".ToCharArray();
            region1.RegionScript = "regireso".ToCharArray();
            region1.AlternativeLocation = new AREPoint() {X = 200, Y = 200};
            region1.UnknownData2 = new byte[56];
            region1.DialogFile = "dlgresou".ToCharArray();

            var region2 = region1;
            region2.Name = "example region 2".PadRight(32).ToCharArray();
            region2.Trap = new ARETrap()
            {
                TrapDetectionDifficulty = 0,
                TrapRemovalDifficulty = 0,
                IsTrapped = 0,
                IsTrapDetected = 0,
                TrapLocation = new AREPoint() { X = 0, Y = 0 }
            };

            var expectedOutput = new List<ARERegion>() {region1, region2};

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.RegionCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadRegions", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_create_spawn_points()
        {
            var spawnPoint1 = new ARESpawnPoint();
            spawnPoint1.Name = "example spawn point 1".PadRight(32).ToCharArray();
            spawnPoint1.Location = new AREPoint() {X = 100, Y = 100};
            spawnPoint1.CreatureReference1 = "mobspawn".ToCharArray();
            spawnPoint1.CreatureReference2 = 
            spawnPoint1.CreatureReference3 = 
            spawnPoint1.CreatureReference3 =                                                               
            spawnPoint1.CreatureReference4 =
            spawnPoint1.CreatureReference5 =
            spawnPoint1.CreatureReference6 =
            spawnPoint1.CreatureReference7 =
            spawnPoint1.CreatureReference8 =
            spawnPoint1.CreatureReference9 =
            spawnPoint1.CreatureReference10 = new char[8];
            
            spawnPoint1.UnknownData3 = new byte[56];

            var spawnPoint2 = spawnPoint1;
            spawnPoint2.Name = "example spawn point 2".PadRight(32).ToCharArray();
            spawnPoint2.CreatureReference2 = "mobspaw2".ToCharArray();

            var expectedOutput = new List<ARESpawnPoint>() { spawnPoint1, spawnPoint2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.SpawnPointCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadSpawnPoints", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_create_entrances()
        {
            var entrance1 = new AREEntrance();
            entrance1.Name = "example entrance 1".PadRight(32).ToCharArray();
            entrance1.Location = new AREPoint() {X = 100, Y = 100};
            entrance1.Orientation = 25;
            entrance1.UnknownData1 = new byte[66];

            var entrance2 = entrance1;
            entrance2.Name = "example entrance 2".PadRight(32).ToCharArray();

            var expectedOutput = new List<AREEntrance>() { entrance1, entrance2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.EntranceCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadEntrances", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_create_containers()
        {
            var container1 = new AREContainer();
            container1.Name = "example container 1".PadRight(32).ToCharArray();
            container1.Location = new AREPoint() {X = 100, Y = 100};
            container1.ContainerType = 0x2;
            container1.LockDifficulty = 50;
            container1.ContainerFlags = 0x10;
            container1.Trap = new ARETrap()
                                  {
                                      TrapDetectionDifficulty = 25,
                                      TrapRemovalDifficulty = 30,
                                      IsTrapped = 1,
                                      IsTrapDetected = 0,
                                      TrapLocation = new AREPoint() {X = 100, Y = 100}
                                  };
            container1.MinimumBoundingBox = new ARERect() {top = 10, right = 20, bottom = 30, left = 40};
            container1.ItemIndex = 1;
            container1.ItemCount = 20;
            container1.TrapScript = "trapreso".ToCharArray();
            container1.VertexIndex = 1;
            container1.VertexCount = 4;
            container1.UnknownData1 = new byte[32];
            container1.KeyItem = "keyitem1".ToCharArray();
            container1.LockpickLinkId = 0xFFC421D;
            container1.UnknownData3 = new byte[56];

            var container2 = container1;
            container2.Name = "example container 2".PadRight(32).ToCharArray();
            container2.Location = new AREPoint() {X = 500, Y = 25};
            container2.ContainerType = 0x1;

            var expectedOutput = new List<AREContainer>() { container1, container2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.ContainerCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadContainers", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_create_items()
        {
            var item1 = new AREItem();
            item1.ItemReference = "item1ref".ToCharArray();
            item1.ItemExpirationTime = 2034;
            item1.Quantity1 = 1;
            item1.ItemFlags = 0x1;

            var item2 = item1;
            item2.ItemReference = "item2ref".ToCharArray();
            item2.ItemExpirationTime = 10;

            var expectedOutput = new List<AREItem>() { item1, item2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.ItemCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadItems", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_read_verticies()
        {
            var expectedOutput = new List<AREPoint>()
                                     {
                                         new AREPoint() {X = 10, Y = 10},
                                         new AREPoint() {X = 30, Y = 100},
                                         new AREPoint() {X = 100, Y = 100}
                                     };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.VerticeCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadVertices", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_read_ambients()
        {
            var ambient1 = new AREAmbient();
            ambient1.Name = "ambinam1".ToCharArray();
            ambient1.Location = new AREPoint() {X = 24, Y = 46};
            ambient1.Radius = 20;
            ambient1.Height = 50;
            ambient1.UnknownData1 = new byte[6];
            ambient1.Volume = 100;
            ambient1.SoundReference1 = "sndref01".ToCharArray();
            ambient1.SoundReference2 =
            ambient1.SoundReference3 =
            ambient1.SoundReference4 =
            ambient1.SoundReference5 =
            ambient1.SoundReference6 =
            ambient1.SoundReference7 =
            ambient1.SoundReference8 =
            ambient1.SoundReference9 =
            ambient1.SoundReference10 = new char[8];
            ambient1.SoundCount = 1;
            ambient1.TimeIntervalInSeconds = 2;
            ambient1.TimeDeviation = 5;
            ambient1.AppearenceSchedule = 0x1;
            ambient1.AmbientFlags = 0x2;
            ambient1.UnknownData3 = new byte[64];

            var ambient2 = ambient1;
            ambient2.Name = "ambinam2".ToCharArray();
            ambient2.Location = new AREPoint() {X = 100, Y = 90};
            ambient2.TimeIntervalInSeconds = 5;

            var expectedOutput = new List<AREAmbient>() { ambient1, ambient2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.AmbientSoundCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadAmbients", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_read_variables()
        {
            var variable1 = new AREVariable();
            variable1.Name = "example variable 1".PadRight(32).ToCharArray();
            variable1.UnknownData1 = new byte[8];
            variable1.Value = 10;
            variable1.UnknownData2 = new byte[40];

            var variable2 = new AREVariable();
            variable2.Name = "example variable 2".PadRight(32).ToCharArray();
            variable2.UnknownData1 = new byte[8];
            variable2.Value = 32;
            variable2.UnknownData2 = new byte[40];

            var expectedOutput = new List<AREVariable>() { variable1, variable2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.VariableCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadVariables", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        [Ignore]
        public void should_correctly_read_explored_bitmask()
        {
            //TODO find out how to read explored bitmasks
        }

        [Test]
        public void should_read_doors()
        {
            var door1 = new AREDoor();
            door1.Name = "example door 1".PadRight(32).ToCharArray();
            door1.DoorLinkId = "doorres1".ToCharArray();
            door1.DoorFlags.SetBits(true, 0, 7);
            door1.OpenVertexIndex = 0;
            door1.OpenVertexCount = 5;
            door1.ClosedVertexCount = 2;
            door1.ClosedVertexIndex = 1;
            door1.OpenBoundingBox = new ARERect() {top = 10, bottom = 100, right = 23};
            door1.ClosedBoundingBox = new ARERect() {top = 20, bottom = 80, right = 10};
            door1.OpenCellBlockIndex = 0;
            door1.OpenCellBlockCount = 5;
            door1.ClosedCellBlockIndex = 5;
            door1.ClosedCellBlockCount = 3;
            door1.DoorOpenSoundReference = "doorsnd1".ToCharArray();
            door1.DoorClosedSoundReference = "doorsnd5".ToCharArray();
            door1.CursorIndex = 5;
            door1.Trap = new ARETrap()
                                 {
                                     IsTrapDetected = 0,
                                     IsTrapped = 1,
                                     TrapDetectionDifficulty = 24,
                                     TrapLocation = new AREPoint() {X = 10, Y = 100}
                                 };
            door1.KeyItem = "keyitem3".ToCharArray();
            door1.DoorScript = "doorscr5".ToCharArray();
            door1.DetectionDifficulty = 25;
            door1.LockDifficulty = 233;
            door1.LockpickStringId = 0xEDC1234;
            door1.RegionLink = "Some Example Region".PadRight(32).ToCharArray();
            door1.DialogNameId = 0x1DCCC34;
            door1.DialogReference = "diagref5".ToCharArray();


            var door2 = door1;
            door2.Name = "example door 2".PadRight(32).ToCharArray();
            door2.CursorIndex = 3;

            var expectedOutput = new List<AREDoor>() { door1, door2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.DoorCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadDoors", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_read_animations()
        {
            var animation1 = new AREAnimation();
            animation1.Name = "example animation 1".PadRight(32).ToCharArray();
            animation1.Location = new AREPoint() {X = 100, Y = 200};
            animation1.AppearanceSchedule.SetBits(true, 2, 4, 6);
            animation1.AnimationReference = "animref1".ToCharArray();
            animation1.SequenceNumber = 1;
            animation1.FrameNumber = 2;
            animation1.AnimationFlags = 0x1;
            animation1.Height = 16;
            animation1.Transparency = 0xCC;
            animation1.StartingFrame = 0;
            animation1.SkipCycles = 1;
            animation1.PaletteReference = "palref12".ToCharArray();
            animation1.UnknownData1 = 0;

            var animation2 = animation1;
            animation2.Name = "example animation 2".PadRight(32).ToCharArray();
            animation2.AppearanceSchedule.SetBits(true, 1, 3, 5);
            animation2.PaletteReference = "palref25".ToCharArray();

            var expectedOutput = new List<AREAnimation>() { animation1, animation2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.AnimationCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadAnimations", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_read_automap_notes()
        {
            var automapNote1 = new AREAutomapNote();
            automapNote1.Location = new AREPoint() { X = 100, Y = 200 };
            automapNote1.TextReference = 0x12C;
            automapNote1.IsInternalReference = 1;
            automapNote1.MarkerColour = 3;
            automapNote1.NoteCount = 3;
            automapNote1.UnknownData1 = new byte[36];

            var automapNote2 = automapNote1;
            automapNote2.Location = new AREPoint() { X = 10, Y = 200 };
            automapNote2.TextReference = 0x54FC;

            var expectedOutput = new List<AREAutomapNote>() { automapNote1, automapNote2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.AutomapNoteCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadAutomapNotes", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_read_tiled_objects()
        {
            var tiledObject1 = new ARETiledObject();
            tiledObject1.Name = "Example tiled object 1".PadRight(32).ToCharArray();
            tiledObject1.UnknownData1 = new char[8];
            tiledObject1.UnknownData2 = 0;
            tiledObject1.OpenSearchSquareOffset = 0xCCDFFE;
            tiledObject1.OpenSearchSquareCount = 5;
            tiledObject1.ClosedSearchSquareOffset = 0xCADFFE;
            tiledObject1.ClosedSearchSquareCount = 2;
            tiledObject1.UnknownData3 = new byte[48];

            var tiledObject2 = tiledObject1;
            tiledObject2.Name = "Example tiled object 2".PadRight(32).ToCharArray();
            tiledObject2.OpenSearchSquareCount = 2;

            var expectedOutput = new List<ARETiledObject>() { tiledObject1, tiledObject2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.TiledObjectCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadTiledObjects", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_read_projectile_traps()
        {
            var projectileTrap1 = new AREProjectileTrap();
            projectileTrap1.ProjectileReference = "projref1".ToCharArray();
            projectileTrap1.EffectBlockOffset = 0xCCC;
            projectileTrap1.EffectBlockCount = 2;
            projectileTrap1.MissileReference = 0xDD;
            projectileTrap1.UnknownData1 = 0;
            projectileTrap1.Location = new AREPoint(){X = 100, Y = 200};
            projectileTrap1.UnknownData2 = 0;
            projectileTrap1.CasterId = 2;

            var projectileTrap2 = projectileTrap1;
            projectileTrap2.ProjectileReference = "projref2".ToCharArray();
            projectileTrap2.CasterId = 4;

            var expectedOutput = new List<AREProjectileTrap>() { projectileTrap1, projectileTrap2 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.ProjectileTrapCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadProjectileTraps", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_read_song_entries()
        {
            var projectileTrap1 = new ARESongEntry();
            projectileTrap1.DaySongReference = 0X1;
            projectileTrap1.NightSongReference = 0x2;
            projectileTrap1.WinSongReference = 0x3;
            projectileTrap1.BattleSongReference = 0x4;
            projectileTrap1.LoseSongReference = 0x5;
            projectileTrap1.UnknownData1 = new int[5];
            projectileTrap1.MainDayAmbient1 = "wavres01".ToCharArray();
            projectileTrap1.MainDayAmbient2 = "wavres02".ToCharArray();
            projectileTrap1.MainDayAmbientVolume = 100;
            projectileTrap1.MainNightAmbient1 = "wavres03".ToCharArray();
            projectileTrap1.MainNightAmbient2 = "wavres04".ToCharArray();
            projectileTrap1.MainNightAmbientVolume = 100;
            projectileTrap1.ReverbReference = 0xCCD;
            projectileTrap1.UnknownData2 = new byte[60];

            var expectedOutput = new List<ARESongEntry>() { projectileTrap1 };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            descriptor.ProjectileTrapCount = (short)expectedOutput.Count;

            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadSongEntries", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }

        [Test]
        public void should_read_rest_interruptions()
        {
            var restInterruption = new ARERestInterruption();
            restInterruption.Name = "Example rest interruption".PadRight(32).ToCharArray();
            restInterruption.InterruptionExplanationText = new int[10];
            restInterruption.CreatureSpawnReference1 = "csref001".ToCharArray();
            restInterruption.CreatureSpawnReference2 = "csref002".ToCharArray();
            restInterruption.CreatureSpawnReference3 = "csref003".ToCharArray();
            restInterruption.CreatureSpawnReference4 = "csref004".ToCharArray();
            restInterruption.CreatureSpawnReference5 = "csref005".ToCharArray();
            restInterruption.CreatureSpawnReference6 = new char[8];
            restInterruption.CreatureSpawnReference7 = new char[8];
            restInterruption.CreatureSpawnReference8 = new char[8];
            restInterruption.CreatureSpawnReference9 = new char[8];
            restInterruption.CreatureSpawnReference10 = new char[8];
            restInterruption.CreatureSpawnCount = 5;
            restInterruption.CreatureSpawnFrequency = 25;
            restInterruption.UnknownData1 = 0;
            restInterruption.UnknownData2 = 0;
            restInterruption.MaximumCreatureSpawnCount = 3;
            restInterruption.UnknownData3 = 0;
            restInterruption.ProbabilityDay = 45;
            restInterruption.ProbabilityNight = 60;
            restInterruption.UnknownData4 = new byte[56];

            var expectedOutput = new List<ARERestInterruption>() { restInterruption };

            var plugin = new AREPlugin();
            var descriptor = new AREDescriptor();
            var memoryStream = new MemoryStream();
            memoryStream.WriteStructs(expectedOutput);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);

            var createMethod = plugin.GetType().GetMethod("ReadRestInterruptions", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = createMethod.Invoke(plugin, new object[] { binaryReader, descriptor });

            Assert.That(result, Is.EquivalentTo(expectedOutput));
        }
    }
}
