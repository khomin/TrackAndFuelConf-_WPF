using System;
using System.Runtime.InteropServices;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TrackerStructureSettingsLls
    {
        // lls 1
        [MarshalAs(UnmanagedType.U2)] // TODO: not used
        public UInt16 LevelThresholdLls1;

        [MarshalAs(UnmanagedType.U2)] // TODO: not used
        public UInt16 DrainThresholdLls1;

        [MarshalAs(UnmanagedType.U2)] // TODO: not used
        public UInt16 FillUpThresholdLls1;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 CntEmptyLls1;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 CntFullLls1;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 MinLevelLls1;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 MaxLevelLls1;

        [MarshalAs(UnmanagedType.U1)]
        public Byte OutTypeLls1;

        [MarshalAs(UnmanagedType.U1)]
        public Byte FlterTypeLls1;

        [MarshalAs(UnmanagedType.U1)]
        public Byte AvarageLenghLls1;

        [MarshalAs(UnmanagedType.U1)]
        public Byte MedianLenghtLls1;

        [MarshalAs(UnmanagedType.R4)]
        public float QLls1;

        [MarshalAs(UnmanagedType.R4)]
        public float RLls1;

        [MarshalAs(UnmanagedType.U1)]
        public Byte ThermocompTypeLls1;

        [MarshalAs(UnmanagedType.R4)]
        public float K1Lls1;

        [MarshalAs(UnmanagedType.R4)]
        public float K2Lls1;

        [MarshalAs(UnmanagedType.U1)]
        public Byte InterpolationTypeLls1;

        [MarshalAs(UnmanagedType.U1)]
        public Byte WaterModeTypeLls1;

        // lls 2
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 LevelThresholdLls2;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 DrainThresholdLls2;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 FillUpThresholdLls2;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 CntEmptyLls2;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 CntFullLls2;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 MinLevelLls2;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 MaxLevelLls2;

        [MarshalAs(UnmanagedType.U1)]
        public Byte OutTypeLls2;

        [MarshalAs(UnmanagedType.U1)]
        public Byte FlterTypeLls2;

        [MarshalAs(UnmanagedType.U1)]
        public Byte AvarageLenghLls2;

        [MarshalAs(UnmanagedType.U1)]
        public Byte MedianLenghtLls2;

        [MarshalAs(UnmanagedType.R4)]
        public float QLls2;

        [MarshalAs(UnmanagedType.R4)]
        public float RLls2;

        [MarshalAs(UnmanagedType.U1)]
        public Byte ThermocompTypeLls2;

        [MarshalAs(UnmanagedType.R4)]
        public float K1Lls2;

        [MarshalAs(UnmanagedType.R4)]
        public float K2Lls2;

        [MarshalAs(UnmanagedType.U1)]
        public Byte InterpolationTypeLls2;

        [MarshalAs(UnmanagedType.U1)]
        public Byte WaterModeTypeLls2;
    }
}
