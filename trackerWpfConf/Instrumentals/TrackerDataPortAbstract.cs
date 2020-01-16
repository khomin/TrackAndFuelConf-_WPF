using System;
using System.Collections.Generic;

namespace trackerWpfConf.Instrumentals
{
    abstract class TrackerDataPortAbstract
    {
        public delegate bool CallBack(List<int> data);
        public abstract bool Open(Dictionary<string, object> property, Action<List<int>> callBack);
        public abstract void Close();
    }
}
