using System;
using System.Runtime.InteropServices;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Sequential, Size = 38, Pack = 1)]
    public class TrackerStructureSettingsSms
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Phone1Number;

        [MarshalAs(UnmanagedType.Bool)]
        public bool Phone1IsEnable;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Phone2Number;

        [MarshalAs(UnmanagedType.Bool)]
        public bool Phone2IsEnable;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 EventsPhone1;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 EventsPhone2;
    }
}
