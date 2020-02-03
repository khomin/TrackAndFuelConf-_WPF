using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackAndFuel.Instrumentals
{
    class TrackerSimulationPort : TrackerDataPortAbstract
    {
        private System.Timers.Timer testStatusTimer;
        private bool _settingsIsWrited = false;
        public override bool Open(Dictionary<string, object> property, Action<List<byte>> updateDataCallback, Action disconnectCallback)
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

                if (_settingsIsWrited) 
                {
                    _settingsIsWrited = false;

                    /* About accept new settings */
                    updateDataCallback.Invoke(GetPacketAboutNewSettings());
                }
            };
            return result;
        }
        public override void Close()
        {
            testStatusTimer.Stop();
        }
        public override bool WriteData(string dataHintOptional, byte[] data)
        {
            bool result = false;
            if (dataHintOptional.Contains("writeSettings"))
            {
                System.Timers.Timer testStatusTimer;
                testStatusTimer = new System.Timers.Timer(1500);
                testStatusTimer.AutoReset = false;
                testStatusTimer.Enabled = true;
                testStatusTimer.Elapsed += (sourse, evendt) =>
                {
                    _settingsIsWrited = true;
                    testStatusTimer.Stop();
                };
            }
            return result;
        }

        private List<int> GetNextStatusPacket()
        {
            //byte[] test = { 0x24, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x16, 0x00, 0x44, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x20, 0x74, 0x61, 0x73, 0x6B, 0x20, 0x77, 0x6F, 0x72, 0x6B, 0x69, 0x6E, 0x67, 0x0D, 0x0A, 0xB3 };
            byte[] test = { 0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }; // 0x8b
            var newData = new List<int>(test.Length);
            for (int i = 0; i < test.Length; i++)
            {
                newData.Add(test[i]);
            }
            return newData;
        }

        private List<byte> GetNextRightPanelPacket()
        {
            var data = new List<byte>();
            var parser = new TrackerParserData();
            data.Add((int)TrackerTypeData.TypePacketData.AsyncData);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.Ain1, Type = typeof(float), Data = (float)new Random().NextDouble() }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.Ain1, Type = typeof(float), Data = (float)new Random().NextDouble() }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.Ain2, Type = typeof(float), Data = (float)new Random().NextDouble() }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.Ain3, Type = typeof(float), Data = (float)new Random().NextDouble() }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.PowerBat, Type = typeof(float), Data = (float)new Random().Next(3, 4) }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.PowerExt, Type = typeof(float), Data = (float)new Random().Next(10, 12) }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.imei, Type = typeof(string), Data = String.Format("12345678953555") }));

            float lat = (float)new Random().Next(50, 60);
            float lon = (float)new Random().Next(20, 30);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.GnssLat, Type = typeof(float), Data = lat }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.GnssLon, Type = typeof(float), Data = lon }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.GnssSat, Type = typeof(int), Data = new Random().Next(0, 10) }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.GsmCsq, Type = typeof(int), Data = new Random().Next(0, 31) }));
            data.Add(Crc8Calc.ComputeChecksum(data.ToArray()));
            return data;
        }

        private List<byte> GetPacketAboutNewSettings()
        {
            var data = new List<byte>();
            var parser = new TrackerParserData();
            data.Add((int)TrackerTypeData.TypePacketData.Answer);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsAcknowledgement, Type = typeof(int), Data = 0 }));
            data.Add(Crc8Calc.ComputeChecksum(data.ToArray()));
            return data;
        }
    }
}
