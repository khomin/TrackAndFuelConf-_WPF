using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TrackAndFuel.Instrumentals.TrackerTypeData;

namespace TrackAndFuel.Instrumentals
{
    abstract class TrackerParserDataAbstract
    {
        protected byte[] data;
        public class ParserResult
        {
            public TrackerTypeData.TypePacketData type;
            public List<DataItemParam> data;
            public TypeMessage typeMessage;
            public ParserResult(TrackerTypeData.TypePacketData _type, List<DataItemParam> _data, TypeMessage _typeMessage)  {
                type = _type;
                data = _data;
                typeMessage = _typeMessage;
            }
        }

        public abstract ParserResult Parse();
        public abstract ParserResult Parse(byte[] data);
        public abstract ParserResult Parse(List<int> data);
    }
}
