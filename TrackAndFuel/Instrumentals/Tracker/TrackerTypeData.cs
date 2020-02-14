using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    public class TrackerTypeData
    {
        public enum TypePacketData
        {
            Request = 0x31,
            Answer = 0x3e,
            AsyncData = 0x24
        }
        public enum TypeMessage
        {
            Debug = 0x00,
            Status = 0x01,
            Data = 0x02,
            AsyncData = 0x03,
            SettingsRead = 0x04,
            SettignsWrite = 0x05,
            Log = 0x06,
            LogCleanFinish = 0x07,
            Undefined = 0xff
        }

        public enum KeyParameter
        {
            DbgLevel = 0x00,
            DbgMessage = 0x01,
            imei = 0x02,
            imsi = 0x03,
            GsmCsq = 0x04,
            GnssLat = 0x05,
            GnssLon = 0x06,
            GnssSat = 0x07,
            Ain1 = 0x08,
            Ain2 = 0x09,
            Ain3 = 0x0A,
            PowerBat = 0x0B,
            PowerExt = 0x0C,
            Time = 0x0d,
            SettingsGsm = 0x40,
            SettingsServers = 0x41,
            SettingsSleep = 0x42,
            SettingsGpio = 0x43,
            SettingsLlsInternal = 0x44,
            SettingsOneWire = 0x45,
            SettingsSms = 0x46,
            SettingsGeofence = 0x47,
            SettingsTrack = 0x48,
            Security = 0x49,
            SettingsAcknowledgement = 0x50,
            SettingsReadAll = 0x51,
            LogRecord = 0x18
        }

        public enum TypeParameter
        {
            ParamTypeInt  = 0x00,
            ParamTypeFloat = 0x01,
            ParamTypeString = 0x02,
            ParamTypeBool = 0x03,
            ParamTypeBinary = 0x04
        }
        public enum PacketField
        {
            PacketHeaderIndex = 0x00,
            PacketLenByteL = 0x01,
            PacketLenByteH = 0x02,
            PacketTypeMessageIndex = 0x03,
            PacketParamCountIndex = 0x04,
            PacketStartDataInPacketIndex = 0x05
        }
    }
}
