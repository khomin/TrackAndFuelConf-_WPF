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
            headerData = 0x31,
            answer = 0x3e,
            asyncData = 0x24
        }

        public enum KeyParameter
        {
            DBG_LEVEL = 0x00,
            DBG_MSG = 0x01
        }

        enum TypeMessage
        {
            Debug = 0x00,
            Status = 0x01,
            Data = 0x02,
            AsyncData = 0x03,
            Settings = 0x04,
            Log = 0x05
        }

        enum TypeData
        {
            Int = 0x00,
            Float = 0x01,
            String = 0x02,
            Boolean = 0x03,
            Binary = 0x04
        }
    }
}
