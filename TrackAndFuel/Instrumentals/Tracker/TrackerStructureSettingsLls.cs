using System;
using System.Runtime.InteropServices;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Explicit, Size = 81, Pack = 1)]
    public struct TrackerStructureSettingsLls
    {
        // lls 1
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(0)]
        public UInt16 LevelThresholdLls1;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(2)]
        public UInt16 DrainThresholdLls1;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(4)]
        public UInt16 FillUpThresholdLls1;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(6)]
        public UInt32 CntEmptyLls1;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(10)]
        public UInt32 CntFullLls1;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(14)]
        public UInt16 MinLevelLls1;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(16)]
        public UInt16 MaxLevelLls1;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(18)]
        public Byte OutTypeLls1;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(19)]
        public Byte FlterTypeLls1;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(20)]
        public Byte AvarageLenghLls1;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(21)]
        public Byte MedianLenghtLls1;

        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(22)]
        public float QLls1;

        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(26)]
        public float RLls1;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(27)]
        public Byte ThermocompTypeLls1;

        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(31)]
        public float K1Lls1;

        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(35)]
        public float K2Lls1;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(39)]
        public Byte InterpolationTypeLls1;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(40)]
        public Byte WaterModeTypeLls1;

        // lls 2
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(41)]
        public UInt16 LevelThresholdLls2;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(43)]
        public UInt16 DrainThresholdLls2;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(45)]
        public UInt16 FillUpThresholdLls2;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(47)]
        public UInt32 CntEmptyLls2;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(51)]
        public UInt32 CntFullLls2;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(55)]
        public UInt16 MinLevelLls2;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(57)]
        public UInt16 MaxLevelLls2;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(58)]
        public Byte OutTypeLls2;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(59)]
        public Byte FlterTypeLls2;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(60)]
        public Byte AvarageLenghLls2;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(61)]
        public Byte MedianLenghtLls2;

        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(62)]
        public float QLls2;

        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(66)]
        public float RLls2;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(70)]
        public Byte ThermocompTypeLls2;

        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(71)]
        public float K1Lls2;

        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(75)]
        public float K2Lls2;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(79)]
        public Byte InterpolationTypeLls2;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(80)]
        public Byte WaterModeTypeLls2;
    }
}
