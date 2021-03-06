﻿using System;
using System.Collections.Generic;

namespace TrackAndFuel.Instrumentals
{
    abstract class TrackerDataPortAbstract
    {
        public delegate bool CallBack(List<byte> data);
        public abstract bool Open(Action<byte[]> updateDataCallback, Action disconnectCallback);
        public abstract void Close();
        public abstract bool WriteData(string dataHintOptional, byte[] data);
    }
}
