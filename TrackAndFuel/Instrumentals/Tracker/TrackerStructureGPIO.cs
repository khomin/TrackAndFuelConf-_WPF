using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TrackerStructureGPIO
    {
        // UIN1
        [MarshalAs(UnmanagedType.U1)]
        public byte IN1_Mode;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN1_delta;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN1_timeBase;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN1_LowLevelUpperThreshold;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN1_HightLevelLowerThreshold;

        [MarshalAs(UnmanagedType.Bool)]
        public bool IN1_IsFiltrationEnable;

        [MarshalAs(UnmanagedType.U1)]
        public byte IN1_AveragingFilterLenght;

        // UIN2
        [MarshalAs(UnmanagedType.U1)]
        public byte IN2_Mode;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN2_delta;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN2_timeBase;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN2_LowLevelUpperThreshold;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN2_HightLevelLowerThreshold;

        [MarshalAs(UnmanagedType.Bool)]
        public bool IN2_IsFiltrationEnable;

        [MarshalAs(UnmanagedType.U1)]
        public byte IN2_AveragingFilterLenght;

        // UIN3
        [MarshalAs(UnmanagedType.U1)]
        public byte IN3_Mode;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN3_delta;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN3_timeBase;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN3_LowLevelUpperThreshold;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 IN3_HightLevelLowerThreshold;

        [MarshalAs(UnmanagedType.Bool)]
        public bool IN3_IsFiltrationEnable;

        [MarshalAs(UnmanagedType.U1)]
        public byte IN3_AveragingFilterLenght;

        // OUT1
        [MarshalAs(UnmanagedType.U1)]
        public byte Out1Mode;

        // OUT2
        [MarshalAs(UnmanagedType.U1)]
        public byte Out2Mode;
    }
}
