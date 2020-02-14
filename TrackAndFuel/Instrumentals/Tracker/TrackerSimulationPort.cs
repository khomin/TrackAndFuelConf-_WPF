using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TrackAndFuel.Instrumentals
{
    class TrackerSimulationPort : TrackerDataPortAbstract
    {
        private System.Timers.Timer testStatusTimer;
        private bool _logLoadIsEnabled = false;
        private static UInt32 _logRecordIdCounter = 0;
        private StructureBinaryConverter _structureConverter = new StructureBinaryConverter();
        private Action<byte[]> _updateDataCallback;
        private static float lat = 0;
        private static float lon = 0;
        public override bool Open(Action<byte[]> updateDataCallback, Action disconnectCallback)
        {
            bool result = false;
            testStatusTimer = new System.Timers.Timer(2000);
            testStatusTimer.AutoReset = true;
            testStatusTimer.Enabled = true;

            _updateDataCallback = updateDataCallback;

            // TODO: remove it
            _updateDataCallback.Invoke(GetNextRightPanelPacket());

            testStatusTimer.Elapsed += (sourse, evendt) =>
            {
                /* The status data packet */
                //updateDataCallback.Invoke(GetNextStatusPacket());

                /* The right panel data packet */
                //updateDataCallback.Invoke(GetNextRightPanelPacket());

                if (_logLoadIsEnabled)
                {
                    _updateDataCallback.Invoke(GetPacketAboutLog());
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
                /* About accept new settings */
                _updateDataCallback.Invoke(GetPacketAboutNewSettings());
            }
            if (dataHintOptional.Contains("readSettings"))
            {
                /* About accept reading the settings */
                _updateDataCallback.Invoke(GetPacketAboutReadSettings());
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

        private byte[] GetNextRightPanelPacket()
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
            var crc = CrcCalc.Crc16(data.ToArray());
            var crcArray = BitConverter.GetBytes(crc);
            data.AddRange(crcArray);
            return data.ToArray();
        }

        private byte[] GetPacketAboutNewSettings()
        {
            var data = new List<byte>();
            var parser = new TrackerParserData();
            data.Add((int)TrackerTypeData.TypePacketData.Answer);
            data.Add((int)TrackerTypeData.TypeMessage.SettignsWrite);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsAcknowledgement, Type = typeof(int), Data = 0 }));
            var crc = CrcCalc.Crc16(data.ToArray());
            var crcArray = BitConverter.GetBytes(crc);
            data.AddRange(crcArray);
            return data.ToArray();
        }

        private byte[] GetPacketAboutReadSettings()
        {
            var data = new List<byte>();
            var parser = new TrackerParserData();
            var converter = new StructureBinaryConverter();
            data.Add((int)TrackerTypeData.TypePacketData.Answer);
            data.Add((int)TrackerTypeData.TypeMessage.SettingsRead);
            
            var settingsGsm = new TrackerStructureGsm();
            settingsGsm.PinCode = Encoding.Default.GetBytes("1234");
            settingsGsm.Apn = Encoding.Default.GetBytes("TestApnSomethingBlabla");
            settingsGsm.ApnUser = Encoding.Default.GetBytes("TestUserName16");
            settingsGsm.ApnPassword = Encoding.Default.GetBytes("11445500991991");

            var settingsConnection = new TrackerStructureSettingsConnection();
            //settingsConnection.ProtocolType = 0;
            //settingsConnection.Connect1Addr = Encoding.Default.GetBytes("locuscomtech.com");
            //settingsConnection.Connect1Password = Encoding.Default.GetBytes("998876655");
            //settingsConnection.Connect1DelayBeforeNextConnecting = 3;
            //settingsConnection.Connect1PeriodOfPingMessage = 3;
            //settingsConnection.Connect1Port = 45454;
            //settingsConnection.Connect1SendingPeropdDuringParking = 3;
            //settingsConnection.Connect1SendingPeropdInSleepMode = 3;
            //settingsConnection.Connect1UseCompression = false;

            var settingsGpio = new TrackerStructureGPIO();
            settingsGpio.IN1_AveragingFilterLenght = 3;
            settingsGpio.IN1_delta = 1234;
            settingsGpio.IN1_HightLevelLowerThreshold = 2;
            settingsGpio.IN1_LowLevelUpperThreshold = 1;
            settingsGpio.IN1_timeBase = 10;
            settingsGpio.IN1_Mode = 2;
            settingsGpio.IN2_AveragingFilterLenght = 3;
            settingsGpio.IN2_delta = 1234;
            settingsGpio.IN2_HightLevelLowerThreshold = 2;
            settingsGpio.IN2_LowLevelUpperThreshold = 1;
            settingsGpio.IN2_timeBase = 10;
            settingsGpio.IN2_Mode = 2;
            settingsGpio.IN3_AveragingFilterLenght = 3;
            settingsGpio.IN3_delta = 1234;
            settingsGpio.IN3_HightLevelLowerThreshold = 2;
            settingsGpio.IN3_LowLevelUpperThreshold = 1;
            settingsGpio.IN3_timeBase = 10;
            settingsGpio.IN3_Mode = 2;

            var settingsLlsInternal = new TrackerStructureSettingsLls();
            settingsLlsInternal.AvarageLenghLls1 = 1;
            settingsLlsInternal.AvarageLenghLls2 = 1;
            settingsLlsInternal.CntEmptyLls1 = 1;
            settingsLlsInternal.CntEmptyLls2 = 1;
            settingsLlsInternal.CntFullLls1 = 1;
            settingsLlsInternal.CntFullLls2 = 1;
            settingsLlsInternal.DrainThresholdLls1 = 1;
            settingsLlsInternal.DrainThresholdLls2 = 1;
            settingsLlsInternal.FillUpThresholdLls1 = 1;
            settingsLlsInternal.FillUpThresholdLls2 = 1;
            settingsLlsInternal.FlterTypeLls1 = 1;
            settingsLlsInternal.FlterTypeLls2 = 1;
            settingsLlsInternal.InterpolationTypeLls1 = 1;
            settingsLlsInternal.InterpolationTypeLls2 = 1;
            settingsLlsInternal.K1Lls1 = 1;
            settingsLlsInternal.K1Lls2 = 1;
            settingsLlsInternal.K2Lls1 = 1;
            settingsLlsInternal.K2Lls2 = 1;
            settingsLlsInternal.LevelThresholdLls1 = 1;
            settingsLlsInternal.LevelThresholdLls2 = 1;
            settingsLlsInternal.MaxLevelLls1 = 1;
            settingsLlsInternal.MaxLevelLls2 = 1;
            settingsLlsInternal.MedianLenghtLls1 = 1;
            settingsLlsInternal.MedianLenghtLls2 = 1;
            settingsLlsInternal.MinLevelLls1 = 1;
            settingsLlsInternal.MinLevelLls2 = 1;
            settingsLlsInternal.OutTypeLls1 = 1;
            settingsLlsInternal.OutTypeLls2 = 1;
            settingsLlsInternal.QLls1 = 1;
            settingsLlsInternal.QLls2 = 1;
            settingsLlsInternal.RLls2 = 1;
            settingsLlsInternal.ThermocompTypeLls1 = 1;
            settingsLlsInternal.ThermocompTypeLls2 = 1;
            settingsLlsInternal.WaterModeTypeLls1 = 1;
            settingsLlsInternal.WaterModeTypeLls2 = 1;

            var settingsOneWire = new TrackerStructureSettingsOneWire();
            settingsOneWire.Sensor1IsEnabled = true;
            settingsOneWire.Sensor1_AlarmZoneMax = 30;
            settingsOneWire.Sensor1_AlarmZoneMin = 5;
            settingsOneWire.Sensor1_Code = Encoding.Default.GetBytes("112233445566778899");
            settingsOneWire.Sensor1_Name = Encoding.Default.GetBytes("sen1");

            var settingsSms = new TrackerStructureSettingsSms();
            settingsSms.EventsPhone1 = 4;
            settingsSms.Phone1IsEnable = true;
            settingsSms.Phone1Number = Encoding.Default.GetBytes("8903664167");

            var settingsTrack = new TrackerStructureSettingsTrack();

            data.AddRange(parser.addParam(new DataItemParam
            {
                Key = TrackerTypeData.KeyParameter.SettingsGsm,
                Type = typeof(byte[]),
                Data = converter.Serialize(settingsGsm)
            }));
            data.AddRange(parser.addParam(new DataItemParam
            {
                Key = TrackerTypeData.KeyParameter.SettingsGpio,
                Type = typeof(byte[]),
                Data = converter.Serialize(settingsGpio)
            }));
            data.AddRange(parser.addParam(new DataItemParam
            {
                Key = TrackerTypeData.KeyParameter.SettingsLlsInternal,
                Type = typeof(byte[]),
                Data = converter.Serialize(settingsLlsInternal)
            }));
            data.AddRange(parser.addParam(new DataItemParam
            {
                Key = TrackerTypeData.KeyParameter.SettingsOneWire,
                Type = typeof(byte[]),
                Data = converter.Serialize(settingsOneWire)
            }));
            data.AddRange(parser.addParam(new DataItemParam
            {
                Key = TrackerTypeData.KeyParameter.SettingsSms,
                Type = typeof(byte[]),
                Data = converter.Serialize(settingsSms)
            }));
            data.AddRange(parser.addParam(new DataItemParam
            {
                Key = TrackerTypeData.KeyParameter.SettingsTrack,
                Type = typeof(byte[]),
                Data = converter.Serialize(settingsTrack)
            }));
            var crc = CrcCalc.Crc16(data.ToArray());
            var crcArray = BitConverter.GetBytes(crc);
            data.AddRange(crcArray);
            return data.ToArray();
        }

        private byte[] GetPacketAboutLog()
        {
            var data = new List<byte>();
            var parser = new TrackerParserData();
            data.Add((int)TrackerTypeData.TypePacketData.AsyncData);
            data.Add((int)TrackerTypeData.TypeMessage.Log);
            TrackerStructureLogRecord record = new TrackerStructureLogRecord();//_structureConverter.fromBytes<TrackerStructureLogRecord>(logRecord);
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
                Data = _structureConverter.Serialize(record)
            }));
            var crc = CrcCalc.Crc16(data.ToArray());
            var crcArray = BitConverter.GetBytes(crc);
            data.AddRange(crcArray);
            return data.ToArray();
        }
    }
}
