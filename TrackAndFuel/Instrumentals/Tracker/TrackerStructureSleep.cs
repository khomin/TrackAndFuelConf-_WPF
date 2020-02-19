using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TrackerStructureSleep
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool SleepIsEnabled;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 SleepVoltageThreshold;
    }
}
