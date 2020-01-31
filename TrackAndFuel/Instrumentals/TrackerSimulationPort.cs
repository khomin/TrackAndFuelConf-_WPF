using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackAndFuel.Instrumentals
{
    class TrackerSimulationPort : TrackerDataPortAbstract
    {
        private System.Timers.Timer testStatusTimer;
        public TrackerSimulationPort()
        {

        }
        public override bool Open(Dictionary<string, object> property, Action<List<int>> updateDataCallback, Action disconnectCallback)
        {
            bool result = false;
            testStatusTimer = new System.Timers.Timer(2000);
            testStatusTimer.AutoReset = true;
            testStatusTimer.Enabled = true;
            testStatusTimer.Elapsed += (sourse, evendt) =>
            {
                /* The status data packet */
                //updateDataCallback.Invoke(GetNextStatusPacket());

                /* The right panel data packet */
                updateDataCallback.Invoke(GetNextRightPanelPacket());
            };
            return result;
        }
        public override void Close()
        {
            testStatusTimer.Stop();
        }
        public override bool WriteData(byte[] data)
        {
            throw new NotImplementedException();
        }

        private List<int> GetNextStatusPacket()
        {
            byte[] test = { 0x24, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x16, 0x00, 0x44, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x20, 0x74, 0x61, 0x73, 0x6B, 0x20, 0x77, 0x6F, 0x72, 0x6B, 0x69, 0x6E, 0x67, 0x0D, 0x0A, 0xB3 };
            var newData = new List<int>(test.Length);
            for (int i = 0; i < test.Length; i++)
            {
                newData.Add(test[i]);
            }
            return newData;
        }

        private List<int> GetNextRightPanelPacket()
        {
            var data = new List<int>();

            data.Add((int)TrackerTypeData.TypePacketData.AsyncData);
            //data.Add((int)TrackerTypeData.TypeMessage.Debug);

            data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.DbgLevel, Type = typeof(string), Data = String.Format("Default task working\r\n") })) ;
            
            var crc = Crc8Calc.ComputeChecksum(data.Select(i => BitConverter.GetBytes(i)));
            data.Add(crc);
            return data;

            //var data = new List<int>();

            //data.Add((int)TrackerTypeData.TypePacketData.AsyncData);
            //data.Add((int)TrackerTypeData.TypeMessage.Data);

            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.Ain1, Type = typeof(float), Data = new Random().NextDouble() })));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.Ain1, Type = typeof(float), Data = new Random().NextDouble() }));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.Ain2, Type = typeof(float), Data = new Random().NextDouble() }));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.Ain3, Type = typeof(float), Data = new Random().NextDouble() }));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.PowerBat, Type = typeof(float), Data = new Random().Next(3, 4) }));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.PowerExt, Type = typeof(float), Data = new Random().Next(10, 12) }));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.imei, Type = typeof(string), Data = String.Format("12345678953555") }));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.GnssLat, Type = typeof(float), Data = new Random().NextDouble() }));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.GnssLon, Type = typeof(float), Data = new Random().NextDouble() }));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.GnssSat, Type = typeof(int), Data = new Random().Next(0, 10) }));
            //data.AddRange(addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.GsmCsq, Type = typeof(int), Data = new Random().Next(0, 31) }));

            //var crc = Crc8Calc.ComputeChecksum(data.Select(i => BitConverter.GetBytes(i)));
            //data.Add(crc);
            //return data;
        }

        private List<int> addParam(DataItemParam data)
        {
            List<int> res = new List<int>();
            res.Add((int)data.Key);

            if (data.Type == typeof(int))
            {
                res.Add((int)TrackerTypeData.TypeParameter.ParamTypeInt);
                BitConverter.GetBytes((int)data.Data);
            }
            else if (data.Type == typeof(bool))
            {
                res.Add((int)TrackerTypeData.TypeParameter.ParamTypeBool);
                res.Add((int)data.Data);
            }
            else if (data.Type == typeof(string))
            {
                res.Add((int)TrackerTypeData.TypeParameter.ParamTypeString);
                res.Add(data.Data.ToString().Length);
                foreach (var i in data.Data.ToString()) 
                {
                    res.Add(i);
                }
            }
            else if (data.Type == typeof(float))
            {
                res.Add((int)TrackerTypeData.TypeParameter.ParamTypeFloat);
            }
            else if (data.Type == typeof(byte[]))
            {
                res.Add((int)TrackerTypeData.TypeParameter.ParamTypeBinary);
                var d = (byte[])data.Data;
                res.Add(d.Length);
            }
            return res;
        }
    }
}
