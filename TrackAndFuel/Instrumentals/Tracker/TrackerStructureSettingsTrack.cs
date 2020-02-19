using System;
using System.Runtime.InteropServices;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Explicit, Size = 31, Pack = 1)]
    public class TrackerStructureSettingsTrack
    {
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(0)]
        public UInt16 MaxDistance;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(2)]
        public UInt16 MaxHeading;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(4)]
        public UInt16 SendTimeStop;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(6)]
        public UInt16 SendTimeSleep;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(9)]
        public byte AccelThresholdSleep;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(10)]
        public UInt16 StopToMoveSleep;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(12)]
        public UInt16 MoveToStopSleep;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(14)]
        public UInt32 MaxStendingTime;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(18)]
        public UInt16 MaxSpeep;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(20)]
        public UInt16 MinSpeep;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(22)]
        public UInt16 PosAccel;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(24)]
        public UInt16 NegAccel;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(26)]
        public byte IgnType;

        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(27)]
        public float IgnThreshold;
    }
}
