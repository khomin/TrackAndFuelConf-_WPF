using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 1)]
    public struct TrackerStructureSettingsConnection
    {
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(0)]
        public byte ProtocolType;

        [MarshalAs(UnmanagedType.LPArray)]
        [FieldOffset(1)]
        public byte[] Connect1Addr;

        [MarshalAs(UnmanagedType.LPArray)]
        [FieldOffset(65)]
        public byte[] Connect1Password;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(81)]
        public UInt16 Connect1Port;

        [MarshalAs(UnmanagedType.Bool)]
        [FieldOffset(82)]
        public bool Connect1UseCompression;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(83)]
        public UInt32 Connect1PeriodOfPingMessage;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(86)]
        public UInt32 Connect1DelayBeforeNextConnecting;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(90)]
        public UInt32 Connect1SendingPeropdDuringParking;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(94)]
        public UInt32 Connect1SendingPeropdInSleepMode;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(100)]
        public UInt32 AdditionParams;
    }
}
