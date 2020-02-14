using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Sequential, Size = 104, Pack = 1)]
    public struct TrackerStructureSettingsConnection
    {
        /* first connect */

        [MarshalAs(UnmanagedType.U1)]
        public byte ProtocolType_0;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 64)]
        public byte[] ConnectAddr_0;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 16)]
        public byte[] ConnectPassword_0;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 ConnectPort_0;

        [MarshalAs(UnmanagedType.Bool)]
        public bool ConnectUseCompression_0;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ConnectPeriodOfPingMessage_0;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ConnectDelayBeforeNextConnecting_0;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ConnectSendingPeropdDuringParking_0;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ConnectSendingPeropdInSleepMode_0;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 AdditionParams_0;

        /* second connect */

        [MarshalAs(UnmanagedType.U1)]
        public byte ProtocolType_1;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 64)]
        public byte[] ConnectAddr_1;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 16 )]
        public byte[] ConnectPassword_1;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 ConnectPort_1;

        [MarshalAs(UnmanagedType.Bool)]
        public bool ConnectUseCompression_1;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ConnectPeriodOfPingMessage_1;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ConnectDelayBeforeNextConnecting_1;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ConnectSendingPeropdDuringParking_1;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ConnectSendingPeropdInSleepMode_1;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 AdditionParams_1;
    }
}
