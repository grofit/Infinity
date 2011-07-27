using System.Runtime.InteropServices;
using netextender.extensions;

namespace Infinity.Plugins.ARE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ARESongEntry
    {
        public int DaySongReference;
        public int NightSongReference;
        public int WinSongReference;
        public int BattleSongReference;
        public int LoseSongReference;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public int[] UnknownData1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] MainDayAmbient1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] MainDayAmbient2;
        public int MainDayAmbientVolume;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] MainNightAmbient1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] MainNightAmbient2;
        public int MainNightAmbientVolume;

        public int ReverbReference;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
        public byte[] UnknownData2;

        public override bool Equals(object obj)
        {
            if (!(obj is ARESongEntry))
            { return base.Equals(obj); }

            var castObj = (ARESongEntry)obj;

            return castObj.DaySongReference == DaySongReference &&
                   castObj.NightSongReference == NightSongReference &&
                   castObj.WinSongReference == WinSongReference &&
                   castObj.BattleSongReference == BattleSongReference &&
                   castObj.BattleSongReference == BattleSongReference &&
                   castObj.LoseSongReference == LoseSongReference &&
                   castObj.UnknownData1.SameAs(UnknownData1) &&
                   castObj.MainDayAmbient1.SameAs(MainDayAmbient1) &&
                   castObj.MainDayAmbient2.SameAs(MainDayAmbient2) &&
                   castObj.MainDayAmbientVolume == MainDayAmbientVolume &&
                   castObj.MainNightAmbient1.SameAs(MainNightAmbient1) &&
                   castObj.MainNightAmbient2.SameAs(MainNightAmbient2) &&
                   castObj.MainNightAmbientVolume == MainNightAmbientVolume &&
                   castObj.ReverbReference == ReverbReference && 
                   castObj.UnknownData2.SameAs(UnknownData2);
                   
        }
    }
}
