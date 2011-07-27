using System.Runtime.InteropServices;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ARETrap
    {
        /// <summary>
        /// Trap detection difficulty as a %
        /// </summary>
        public short TrapDetectionDifficulty;

        /// <summary>
        /// Trap removal difficulty as a %
        /// </summary>
        public short TrapRemovalDifficulty;

        public short IsTrapped;
        public short IsTrapDetected;
        public AREPoint TrapLocation;

        public override bool Equals(object obj)
        {
            if (!(obj is ARETrap))
            { return base.Equals(obj); }

            var castObj = (ARETrap)obj;

            return castObj.TrapDetectionDifficulty == TrapDetectionDifficulty &&
                   castObj.TrapRemovalDifficulty == TrapRemovalDifficulty &&
                   castObj.IsTrapped == IsTrapped && castObj.IsTrapDetected == IsTrapDetected &&
                   castObj.TrapLocation.Equals(TrapLocation);
        }
    }
}
