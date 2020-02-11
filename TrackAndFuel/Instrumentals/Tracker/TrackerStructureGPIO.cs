using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct TrackerStructureGPIO
    {
        // UIN1
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(0)]
        public byte IN1_IsEnable;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(1)]
        public UInt16 IN1_delta;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(3)]
        public UInt16 IN1_timeBase;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(5)]
        public UInt16 IN1_LowLevelUpperThreshold;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(7)]
        public UInt16 IN1_HightLevelLowerThreshold;

        [MarshalAs(UnmanagedType.Bool)]
        [FieldOffset(9)]
        public bool IN1_IsFiltrationEnable;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(10)]
        public byte IN1_AveragingFilterLenght;

        // UIN2
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(11)]
        public byte IN2_IsEnable;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(12)]
        public UInt16 IN2_delta;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(14)]
        public UInt16 IN2_timeBase;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(16)]
        public UInt16 IN2_LowLevelUpperThreshold;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(18)]
        public UInt16 IN2_HightLevelLowerThreshold;

        [MarshalAs(UnmanagedType.Bool)]
        [FieldOffset(19)]
        public bool IN2_IsFiltrationEnable;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(20)]
        public byte IN2_AveragingFilterLenght;

        // UIN3
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(21)]
        public byte IN3_IsEnable;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(22)]
        public UInt16 IN3_delta;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(24)]
        public UInt16 IN3_timeBase;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(26)]
        public UInt16 IN3_LowLevelUpperThreshold;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(28)]
        public UInt16 IN3_HightLevelLowerThreshold;

        [MarshalAs(UnmanagedType.Bool)]
        [FieldOffset(30)]
        public bool IN3_IsFiltrationEnable;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(31)]
        public byte IN3_AveragingFilterLenght;

        // OUT1
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(32)]
        public byte Out1Mode;

        // OUT2
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(33)]
        public byte Out2Mode;
    }
}
