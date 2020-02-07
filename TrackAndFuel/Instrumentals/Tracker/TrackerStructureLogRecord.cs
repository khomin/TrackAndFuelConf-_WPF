using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Explicit, Size = 56, Pack = 1)]
    public struct TrackerStructureLogRecord
    {
        /* record id */
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        public UInt32 Id;

        /* datetime */
        [MarshalAs(UnmanagedType.U8)]
        [FieldOffset(4)]
        public long DateTimestamp;

        /* event mask */
        [MarshalAs(UnmanagedType.U8)]
        [FieldOffset(12)]
        public UInt64 EventHistory;

        /* odometer */
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(16)]
        public UInt32 Odometr;

        /* gnss is valid */
        [MarshalAs(UnmanagedType.Bool)]
        [FieldOffset(17)]
        public bool GnssRecordIsValid;

        /* gnss longitude */
        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(21)]
        public float GnssLongitude;

        /* gnss latitude */
        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(25)]
        public float GnssLatitude;

        /* gnss altitude */
        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(27)]
        public Int16 GnssAltitude;

        /* gnss fix */
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(28)]
        public byte GnssFixStatus;

        /* gnss heading */
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(30)]
        public UInt16 GnssHeading;

        /* gnss speed */
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(32)]
        public UInt16 GnssSpeed;

        /* gnss hdop */
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(34)]
        public UInt16 GnssHdop;

        /* gnss sats count */
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(35)]
        public byte GnssSatsCount;

        /* gsm signal strenght */
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(36)]
        public byte GsmSignalStrenght;

        /* power ain1 */
        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(40)]
        public float AdcAin1;

        /* power ain2 */
        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(44)]
        public float AdcAin2;

        /* power ain3 */
        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(48)]
        public float AdcAin3;

        /* power external */
        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(52)]
        public float AdcPowerExternal;

        /* power internal */
        [MarshalAs(UnmanagedType.R4)]
        [FieldOffset(56)]
        public float AdcPowerInternal;

        /* lls internal 1 value */
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(58)]
        public UInt16 LlsInternal_0_Value;

        /* lls internal 1 frequency */
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(60)]
        public UInt16 LlsInternal_0_Frequency;

        /* lls internal 2 value */
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(62)]
        public UInt16 LlsInternal_1_Value;

        /* lls internal 2 frequency */
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(64)]
        public UInt16 LlsInternal_1_Frequency;

        /* onewire tempsesor 1 */
        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(66)]
        public Int16 OneWireTempSensor_0;

        /* onewire tempsesor 2 */
        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(68)]
        public Int16 OneWireTempSensor_1;

        /* onewire tempsesor 3 */
        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(70)]
        public Int16 OneWireTempSensor_2;

        /* onewire tempsesor 4 */
        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(72)]
        public Int16 OneWireTempSensor_3;

        /* record crc */
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(73)]
        public byte CrcRecord;
    }
}
