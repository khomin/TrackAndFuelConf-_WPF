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
            Settings = 0x0D,
            GetSettings = 0x0E,
            SettingsAcknowledgement = 0x0F
        }

        public enum TypeParameter
        {
            ParamTypeInt  = 0x00,
            ParamTypeFloat = 0x01,
            ParamTypeString = 0x02,
            ParamTypeBool = 0x03,
            ParamTypeBinary = 0x04
        }

        public enum TypeMessage
        {
            Debug = 0x00,
            Status = 0x01,
            Data = 0x02,
            AsyncData = 0x03,
            Settings = 0x04,
            Log = 0x05,
            Undefined = 0xff
        }

        public enum PacketField
        {
            TypePacket = 0x00,
            TypeMessage = 0x01,
            ParamsCount = 0x02
        }
    }
}
