using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.Instrumentals
{
    abstract class TrackerParserDataAbstract
    {
        protected byte[] data;
        protected uint crcResult;

        public abstract (TrackerTypeData.TypePacketData type, List<DataItemParam> data) Parse();
        public abstract (TrackerTypeData.TypePacketData type, List<DataItemParam> data) Parse(byte[] data);
        public abstract (TrackerTypeData.TypePacketData type, List<DataItemParam> data) Parse(List<int> data);
    }
}
