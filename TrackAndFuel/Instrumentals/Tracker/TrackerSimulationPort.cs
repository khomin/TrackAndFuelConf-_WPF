using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace TrackAndFuel.Instrumentals
{
    class TrackerSimulationPort : TrackerDataPortAbstract
    {
        private System.Timers.Timer testStatusTimer;
        private bool _settingsIsWrited = false;
        private bool _logLoadIsEnabled = false;
        private static UInt32 _logRecordIdCounter = 0;
        private StructureBinaryConverter _structureConverter = new StructureBinaryConverter();
        private static float lat = 0;
        private static float lon = 0;
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

                if (_logLoadIsEnabled)
                {
                    updateDataCallback.Invoke(GetPacketAboutLog());
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

            result = true;

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

            if (dataHintOptional.Contains("startTestLog"))
            {
                _logLoadIsEnabled = true;
            }

            if (dataHintOptional.Contains("stopTestLog"))
            {
                _logLoadIsEnabled = false;
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
            data.Add((int)TrackerTypeData.TypeMessage.AsyncData);
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
            data.Add((int)TrackerTypeData.TypeMessage.SettingsRead);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsAcknowledgement, Type = typeof(int), Data = 0 }));
            data.Add(Crc8Calc.ComputeChecksum(data.ToArray()));
            return data;
        }

        private List<byte> GetPacketAboutLog()
        {
            var data = new List<byte>();
            var parser = new TrackerParserData();
            data.Add((int)TrackerTypeData.TypePacketData.AsyncData);
            data.Add((int)TrackerTypeData.TypeMessage.Log);
            var logRecord = new byte[256];
            TrackerStructureLogRecord record = _structureConverter.fromBytes<TrackerStructureLogRecord>(logRecord);
            record.AdcAin1 = new Random().Next(0, 31);
            record.AdcAin2 = new Random().Next(0, 31);
            record.AdcAin3 = new Random().Next(0, 31);
            record.AdcPowerExternal = new Random().Next(0, 31);
            record.AdcPowerInternal = new Random().Next(0, 31);

            lat += (float)0.1;
            record.GnssLatitude = (float) (lat);
            record.GnssLongitude = (float)(lon);
            record.Id = _logRecordIdCounter++;
            record.DateTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            data.AddRange(parser.addParam(new DataItemParam
            {
                Key = TrackerTypeData.KeyParameter.LogRecord,
                Type = typeof(byte[]),
                Data = _structureConverter.getBytes(record)
            }));
            data.Add(Crc8Calc.ComputeChecksum(data.ToArray()));
            return data;
        }
    }
}
