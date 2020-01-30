using System;
using System.Collections.Generic;

namespace TrackAndFuel.Instrumentals
{
    abstract class TrackerDataPortAbstract
    {
        public delegate bool CallBack(List<int> data);
        public abstract bool Open(Dictionary<string, object> property, Action<List<int>> updateDataCallback, Action disconnectCallback);
        public abstract void Close();
        public abstract bool WriteData(byte[] data);
    }
}
