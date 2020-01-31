using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    abstract class TrackerParserDataAbstract
    {
        protected byte[] data;
        public class ParserResult
        {
            public TrackerTypeData.TypePacketData type;
            public List<DataItemParam> data;
            public ParserResult(TrackerTypeData.TypePacketData _type, List<DataItemParam> _data)  {
                type = _type;
                data = _data;
            }
        }

        public abstract ParserResult Parse();
        public abstract ParserResult Parse(byte[] data);
        public abstract ParserResult Parse(List<int> data);
    }
}
