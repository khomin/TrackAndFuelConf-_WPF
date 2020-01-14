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

        protected abstract TrackerDataResult Parse();

        protected abstract TrackerDataResult Parse(byte[] data);
    }
}
