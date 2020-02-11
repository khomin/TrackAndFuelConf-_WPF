using System;
using System.Runtime.InteropServices;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Sequential, Size = 100, Pack = 1)]
    public struct TrackerStructureGsm
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

