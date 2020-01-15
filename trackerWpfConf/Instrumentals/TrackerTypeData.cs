using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.Instrumentals
{
    class TrackerTypeData
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
            DbgMessage = 0x01
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
            Header = 0x00,
            TypePacket = 0x01,
            ParamsCount = 0X02
        }
    }
}
