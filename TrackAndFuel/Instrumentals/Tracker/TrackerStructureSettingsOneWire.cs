using System;
using System.Runtime.InteropServices;

namespace TrackAndFuel.Instrumentals
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TrackerStructureSettingsOneWire
    {
        /* sensor 1 */
        [MarshalAs(UnmanagedType.Bool)]
        public bool Sensor1IsEnabled;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Sensor1_Code;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Sensor1_Name;

        [MarshalAs(UnmanagedType.R4)]
        public float Sensor1_AlarmZoneMin;

        [MarshalAs(UnmanagedType.R4)]
        public float Sensor1_AlarmZoneMax;

        /* sensor 2 */
        [MarshalAs(UnmanagedType.Bool)]
        public bool Sensor2IsEnabled;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Sensor2_Code;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Sensor2_Name;

        [MarshalAs(UnmanagedType.R4)]
        public float Sensor2_AlarmZoneMin;

        [MarshalAs(UnmanagedType.R4)]
        public float Sensor2_AlarmZoneMax;

        /* sensor 3 */
        [MarshalAs(UnmanagedType.Bool)]
        public bool Sensor3IsEnabled;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Sensor3_Code;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Sensor3_Name;

        [MarshalAs(UnmanagedType.R4)]
        public float Sensor3_AlarmZoneMin;

        [MarshalAs(UnmanagedType.R4)]
        public float Sensor3_AlarmZoneMax;

        /* sensor 4 */
        [MarshalAs(UnmanagedType.Bool)]
        public bool Sensor4IsEnabled;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Sensor4_Code;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Sensor4_Name;

        [MarshalAs(UnmanagedType.R4)]
        public float Sensor4_AlarmZoneMin;

        [MarshalAs(UnmanagedType.R4)]
        public float Sensor4_AlarmZoneMax;
    }
}
