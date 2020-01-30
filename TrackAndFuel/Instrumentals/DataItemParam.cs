using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    public class DataItemParam
    {
        private Type type;
        private Object data;
        private TrackerTypeData.KeyParameter key;

        internal TrackerTypeData.KeyParameter Key { get => key; set => key = value; }
        internal Type Type { get => type; set => type = value; }
        internal Object Data { get => data; set => data = value; }
    }
}
