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

        public abstract TrackerDataResult Parse();

        public abstract TrackerDataResult Parse(byte[] data);
    }
}
