using Infinity.Plugins.ARE;

namespace TestCommon.Builders.Plugins.ARE
{
    public class AREActorBuilder : IBuilder<AREActor>
    {
        private char[] Name = "An example actor".PadRight(32).ToCharArray();
        private short CurrentX = 100;
        private short CurrentY = 100;
        private short DestinationX = 100;
        private short DestinationY = 200;
        private int ActorFlags;
        private int IsSpawned = 1;
        private int Animation = 0xFF;
        private int Orientation = 0xCE;
        private int RemovalTime = -1;
        private int AppearanceSchedule = 1;
        private int TimesTalkedTo = 100;
        private char[] Dialog = "dialreso".ToCharArray();
        private char[] OverrideScript = "overreso".ToCharArray();
        private char[] GeneralScript = "genereso".ToCharArray();
        private char[] RaceScript = "racereso".ToCharArray();
        private char[] ClassScript = "clasreso".ToCharArray();
        private char[] DefaultScript = "defareso".ToCharArray();
        private char[] SpecificScript = "specreso".ToCharArray();
        private char[] CREFile = "CREFile ".ToCharArray();
        private int CREOffset = 0x5C;
        private int CRESize = 0xFC;

        public AREActorBuilder WithName(char[] name)
        {
            Name = ResizeCharactersIfNeeded(name, 32);
            return this;
        }
        public AREActorBuilder WithDestination(short x, short y)
        {
            DestinationX = x;
            DestinationY = y;
            return this;
        }
        public AREActorBuilder WithCurrent(short x, short y)
        {
            CurrentX = x;
            CurrentY = y;
            return this;
        }
        public AREActorBuilder WithFlags(int flags)
        {
            ActorFlags = flags;
            return this;
        }
        public AREActorBuilder WithSpawnState(bool isSpawned)
        {
            IsSpawned = isSpawned ? 1 : 0;
            return this;
        }
        public AREActorBuilder WithAnimation(int animation)
        {
            Animation = animation;
            return this;
        }
        public AREActorBuilder WithOrientation(int orientation)
        {
            Orientation = orientation;
            return this;
        }
        public AREActorBuilder WithRemovalTime(int removalTime)
        {
            RemovalTime = removalTime;
            return this;
        }
        public AREActorBuilder WithAppearanceSchedule(int appearanceSchedule)
        {
            AppearanceSchedule = appearanceSchedule;
            return this;
        }
        public AREActorBuilder WithTimesTalkedTo(int timesTalkedTo)
        {
            TimesTalkedTo = timesTalkedTo;
            return this;
        }
        public AREActorBuilder WithDialog(char[] dialog)
        { 
            Dialog = ResizeCharactersIfNeeded(dialog, 8);
            return this;
        }
        public AREActorBuilder WithOverrideScript(char[] overrideScript)
        {
            OverrideScript = ResizeCharactersIfNeeded(overrideScript, 8);
            return this;
        }
        public AREActorBuilder WithGeneralScript(char[] generalScript)
        {
            GeneralScript = ResizeCharactersIfNeeded(generalScript, 8);
            return this;
        }
        public AREActorBuilder WithRaceScript(char[] raceScript)
        {
            RaceScript = ResizeCharactersIfNeeded(raceScript, 8);
            return this;
        }
        public AREActorBuilder WithClassScript(char[] classScript)
        {
            ClassScript = ResizeCharactersIfNeeded(classScript, 8);
            return this;
        }
        public AREActorBuilder WithDefaultScript(char[] defaultScript)
        {
            DefaultScript = ResizeCharactersIfNeeded(defaultScript, 8);
            return this;
        }
        public AREActorBuilder WithSpecificScript(char[] specificScript)
        {
            SpecificScript = ResizeCharactersIfNeeded(specificScript, 8);
            return this;
        }
        public AREActorBuilder WithCREFile(char[] creFile)
        {
            CREFile = ResizeCharactersIfNeeded(creFile, 8);
            return this;
        }
        public AREActorBuilder WithCREOffset(int creOffset)
        {
            CREOffset = creOffset;
            return this;
        }
        public AREActorBuilder WithCRESize(int creSize)
        {
            CRESize = creSize;
            return this;
        }

        private char[] ResizeCharactersIfNeeded(char[] characters, int maxSize)
        {
            if (characters.Length < 32 || characters.Length > 32)
            { return new string(characters).PadRight(maxSize).ToCharArray(); }
            return characters;
        }

        public AREActor Build()
        {
            var actor = new AREActor();
            actor.Name = Name;
            actor.CurrentX = CurrentX;
            actor.CurrentY = CurrentY;
            actor.DestinationX = DestinationX;
            actor.DestinationY = DestinationY;
            actor.ActorFlags = ActorFlags;
            actor.IsSpawned = IsSpawned;
            actor.Animation = Animation;
            actor.Orientation = Orientation;
            actor.RemovalTime = RemovalTime;
            actor.AppearanceSchedule = AppearanceSchedule;
            actor.TimesTalkedTo = TimesTalkedTo;
            actor.Dialog = Dialog;
            actor.OverrideScript = OverrideScript;
            actor.GeneralScript = GeneralScript;
            actor.RaceScript = RaceScript;
            actor.ClassScript = ClassScript;
            actor.DefaultScript = DefaultScript;
            actor.SpecificScript = SpecificScript;
            actor.CREFile = CREFile;
            actor.CREOffset = CREOffset;
            actor.CRESize = CRESize;
            actor.UnknownEndData = new byte[128];

            return actor;
        }
    }
}