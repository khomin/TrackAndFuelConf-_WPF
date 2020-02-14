using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TrackerStructureGsm
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] PinCode;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] Apn;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] ApnUser;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] ApnPassword;
    }
}