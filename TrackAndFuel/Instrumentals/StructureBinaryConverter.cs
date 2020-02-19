using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals
{
    class StructureBinaryConverter
    {
        public T Deserialize<T>(byte[] rawData)
        {
            T item = Activator.CreateInstance<T>();
            using (MemoryStream stream = new MemoryStream(rawData, 0, rawData.Length))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                if (typeof(T) == typeof(TrackerStructureGsm))
                {
                    var data = item as TrackerStructureGsm;
                    data.PinCode = reader.ReadBytes(4);
                    data.Apn = reader.ReadBytes(64);
                    data.ApnUser = reader.ReadBytes(16);
                    data.ApnPassword = reader.ReadBytes(16);
                }
                else if (typeof(T) == typeof(TrackerStructureSettingsConnection))
                {
                    var data = item as TrackerStructureSettingsConnection;
                    /* first */
                    data.ProtocolType_0 = reader.ReadByte();
                    data.ConnectAddr_0 = reader.ReadBytes(64);
                    data.ConnectPassword_0 = reader.ReadBytes(16);
                    data.ConnectPort_0 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.ConnectUseCompression_0 = reader.ReadByte() == 0 ? false : true;
                    data.ConnectPeriodOfPingMessage_0 = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                    data.ConnectDelayBeforeNextConnecting_0 = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                    data.ConnectSendingPeropdDuringParking_0 = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                    data.ConnectSendingPeropdInSleepMode_0 = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                    data.AdditionParams_0 = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                }
                else if (typeof(T) == typeof(TrackerStructureSettingsOneWire))
                {
                    var data = item as TrackerStructureSettingsOneWire;
                    // sensor1
                    data.Sensor1IsEnabled = reader.ReadByte() == 0 ? false : true;
                    data.Sensor1_Code = reader.ReadBytes(8);
                    data.Sensor1_Name = reader.ReadBytes(16);
                    data.Sensor1_AlarmZoneMin = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.Sensor1_AlarmZoneMax = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    // sensor2
                    data.Sensor2IsEnabled = reader.ReadByte() == 0 ? false : true;
                    data.Sensor2_Code = reader.ReadBytes(8);
                    data.Sensor2_Name = reader.ReadBytes(16);
                    data.Sensor2_AlarmZoneMin = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.Sensor2_AlarmZoneMax = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    // sensor3
                    data.Sensor3IsEnabled = reader.ReadByte() == 0 ? false : true;
                    data.Sensor3_Code = reader.ReadBytes(8);
                    data.Sensor3_Name = reader.ReadBytes(16);
                    data.Sensor3_AlarmZoneMin = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.Sensor3_AlarmZoneMax = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    // sensor4
                    data.Sensor4IsEnabled = reader.ReadByte() == 0 ? false : true;
                    data.Sensor4_Code = reader.ReadBytes(8);
                    data.Sensor4_Name = reader.ReadBytes(16);
                    data.Sensor4_AlarmZoneMin = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.Sensor4_AlarmZoneMax = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                }
                else if (typeof(T) == typeof(TrackerStructureSettingsLls))
                {
                    var data = item as TrackerStructureSettingsLls;
                    // lls1
                    data.LevelThresholdLls1 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.DrainThresholdLls1 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.FillUpThresholdLls1 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.CntEmptyLls1 = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                    data.CntFullLls1 = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                    data.MinLevelLls1 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.MaxLevelLls1 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.OutTypeLls1 = reader.ReadByte();
                    data.FlterTypeLls1 = reader.ReadByte();
                    data.AvarageLenghLls1 = reader.ReadByte();
                    data.MedianLenghtLls1 = reader.ReadByte();
                    data.QLls1 = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.RLls1 = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.ThermocompTypeLls1 = reader.ReadByte();
                    data.K1Lls1 = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.K2Lls1 = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.InterpolationTypeLls1 = reader.ReadByte();
                    data.WaterModeTypeLls1 = reader.ReadByte();
                    // lls2
                    data.LevelThresholdLls2 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.DrainThresholdLls2 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.FillUpThresholdLls2 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.CntEmptyLls2 = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                    data.CntFullLls2 = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                    data.MinLevelLls2 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.MaxLevelLls2 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.OutTypeLls2 = reader.ReadByte();
                    data.FlterTypeLls2 = reader.ReadByte();
                    data.AvarageLenghLls2 = reader.ReadByte();
                    data.MedianLenghtLls2 = reader.ReadByte();
                    data.QLls2 = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.RLls2 = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.ThermocompTypeLls2 = reader.ReadByte();
                    data.K1Lls2 = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.K2Lls2 = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                    data.InterpolationTypeLls2 = reader.ReadByte();
                    data.WaterModeTypeLls2 = reader.ReadByte();
                }
                else if (typeof(T) == typeof(TrackerStructureSettingsSms))
                {
                    var data = item as TrackerStructureSettingsSms;
                    data.Phone1Number = reader.ReadBytes(16);
                    data.Phone1IsEnable = reader.ReadByte() == 0 ? false : true;
                    data.Phone2Number = reader.ReadBytes(16);
                    data.Phone2IsEnable = reader.ReadByte() == 0 ? false : true;
                    data.EventsPhone1 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.EventsPhone2 = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                }
                else if (typeof(T) == typeof(TrackerStructureSettingsTrack))
                {
                    var data = item as TrackerStructureSettingsTrack;
                    data.MaxDistance = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.MaxHeading = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.SendTimeStop = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.SendTimeSleep = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.AccelThresholdSleep = reader.ReadByte();
                    data.StopToMoveSleep = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.MoveToStopSleep = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.MaxStendingTime = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                    data.MaxSpeep = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.MinSpeep = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.PosAccel = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.NegAccel = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IgnType = reader.ReadByte();
                    data.IgnThreshold = BitConverter.ToSingle(reader.ReadBytes(4), 0);
                }
                else if (typeof(T) == typeof(TrackerStructureSleep))
                {
                    var data = item as TrackerStructureSleep;
                    data.SleepIsEnabled = reader.ReadByte() == 0 ? false : true;
                    data.SleepVoltageThreshold = BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                }
                else if (typeof(T) == typeof(TrackerStructureGPIO))
                {
                    var data = item as TrackerStructureGPIO;
                    /* gpio 1 */
                    data.IN1_Mode = reader.ReadByte();
                    data.IN1_delta = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN1_timeBase = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN1_LowLevelUpperThreshold = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN1_HightLevelLowerThreshold = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN1_IsFiltrationEnable = reader.ReadByte() == 0 ? false : true;
                    data.IN1_AveragingFilterLenght = reader.ReadByte();
                    /* gpio 2 */
                    data.IN2_Mode = reader.ReadByte();
                    data.IN2_delta = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN2_timeBase = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN2_LowLevelUpperThreshold = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN2_HightLevelLowerThreshold = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN2_IsFiltrationEnable = reader.ReadByte() == 0 ? false : true;
                    data.IN2_AveragingFilterLenght = reader.ReadByte();
                    /* gpio 1 */
                    data.IN3_Mode = reader.ReadByte();
                    data.IN3_delta = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN3_timeBase = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN3_LowLevelUpperThreshold = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN3_HightLevelLowerThreshold = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    data.IN3_IsFiltrationEnable = reader.ReadByte() == 0 ? false : true;
                    data.IN3_AveragingFilterLenght = reader.ReadByte();
                    /* out 1 */
                    data.Out1Mode = reader.ReadByte();
                    /* out 2 */
                    data.Out2Mode = reader.ReadByte();
                }
                else
                {
                    throw new InvalidDataException("Undefined type");
                }
            }
            return item;
        }

        public byte[] Serialize<T>(T item)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    if (typeof(T) == typeof(TrackerStructureGsm))
                    {
                        var data = (TrackerStructureGsm)(object)item;
                        var pinCode = new byte[4];
                        var apn = new byte[64];
                        var apnUser = new byte[16];
                        var apnPaswword = new byte[16];
                        data.PinCode.CopyTo(pinCode, 0);
                        data.Apn.CopyTo(apn, 0);
                        data.ApnUser.CopyTo(apnUser, 0);
                        data.ApnPassword.CopyTo(apnPaswword, 0);
                        writer.Write(pinCode);
                        writer.Write(apn);
                        writer.Write(apnUser);
                        writer.Write(apnPaswword);
                    }
                    else if (typeof(T) == typeof(TrackerStructureSettingsConnection))
                    {
                        var data = (TrackerStructureSettingsConnection)(object)item;
                        /* first */
                        byte protType = 0;
                        var connectAddr = new byte[64];
                        var connectPassword = new byte[16];
                        UInt16 connectPort = 0;
                        bool useCompress = false;
                        UInt32 connectPeriodOfPingMessage = 0;
                        UInt32 connectDelayBeforeNextCon = 0;
                        UInt32 connectSendingPeriodDuringParking = 0;
                        UInt32 ConnectSendingPeropdInSleepMode = 0;
                        UInt32 AdditionParams = 0;
                        
                        /* connection 1 */
                        protType = data.ProtocolType_0;
                        data.ConnectAddr_0.CopyTo(connectAddr, 0);
                        data.ConnectPassword_0.CopyTo(connectPassword, 0);
                        connectPort = data.ConnectPort_0;
                        useCompress = data.ConnectUseCompression_0;
                        connectPeriodOfPingMessage = data.ConnectPeriodOfPingMessage_0;
                        connectDelayBeforeNextCon = data.ConnectPeriodOfPingMessage_0;
                        connectSendingPeriodDuringParking = data.ConnectPeriodOfPingMessage_0;
                        ConnectSendingPeropdInSleepMode = data.ConnectSendingPeropdInSleepMode_0;
                        AdditionParams = data.AdditionParams_0;
                        writer.Write(protType);
                        writer.Write(connectAddr);
                        writer.Write(connectPassword);
                        writer.Write(connectPort);
                        writer.Write(useCompress);
                        writer.Write(connectPeriodOfPingMessage);
                        writer.Write(connectDelayBeforeNextCon);
                        writer.Write(connectSendingPeriodDuringParking);
                        writer.Write(ConnectSendingPeropdInSleepMode);
                        
                        /* connection 2 */
                        protType = data.ProtocolType_1;
                        data.ConnectAddr_1.CopyTo(connectAddr, 0);
                        data.ConnectPassword_1.CopyTo(connectPassword, 0);
                        connectPort = data.ConnectPort_1;
                        useCompress = data.ConnectUseCompression_1;
                        connectPeriodOfPingMessage = data.ConnectPeriodOfPingMessage_1;
                        connectDelayBeforeNextCon = data.ConnectPeriodOfPingMessage_1;
                        connectSendingPeriodDuringParking = data.ConnectPeriodOfPingMessage_1;
                        ConnectSendingPeropdInSleepMode = data.ConnectSendingPeropdInSleepMode_1;
                        AdditionParams = data.AdditionParams_1;
                        writer.Write(protType);
                        writer.Write(connectAddr);
                        writer.Write(connectPassword);
                        writer.Write(connectPort);
                        writer.Write(useCompress);
                        writer.Write(connectPeriodOfPingMessage);
                        writer.Write(connectDelayBeforeNextCon);
                        writer.Write(connectSendingPeriodDuringParking);
                        writer.Write(ConnectSendingPeropdInSleepMode);
                    }
                    else if (typeof(T) == typeof(TrackerStructureSettingsOneWire))
                    {
                        var data = (TrackerStructureSettingsOneWire)(object)item;
                        var sensorCode = new byte[8];
                        var sensorName = new byte[16];
                        // sensor1
                        data.Sensor1_Code.CopyTo(sensorCode, 0);
                        data.Sensor1_Name.CopyTo(sensorName, 0);
                        writer.Write(data.Sensor1IsEnabled);
                        writer.Write(sensorCode);
                        writer.Write(sensorName);
                        writer.Write(data.Sensor1_AlarmZoneMin);
                        writer.Write(data.Sensor1_AlarmZoneMax);
                        // sensor2
                        data.Sensor2_Code.CopyTo(sensorCode, 0);
                        data.Sensor2_Name.CopyTo(sensorName, 0);
                        writer.Write(data.Sensor2IsEnabled);
                        writer.Write(sensorCode);
                        writer.Write(sensorName);
                        writer.Write(data.Sensor2_AlarmZoneMin);
                        writer.Write(data.Sensor2_AlarmZoneMax);
                        // sensor3
                        data.Sensor3_Code.CopyTo(sensorCode, 0);
                        data.Sensor3_Name.CopyTo(sensorName, 0);
                        writer.Write(data.Sensor3IsEnabled);
                        writer.Write(sensorCode);
                        writer.Write(sensorName);
                        writer.Write(data.Sensor3_AlarmZoneMin);
                        writer.Write(data.Sensor3_AlarmZoneMax);
                        // sensor4
                        data.Sensor4_Code.CopyTo(sensorCode, 0);
                        data.Sensor4_Name.CopyTo(sensorName, 0);
                        writer.Write(data.Sensor4IsEnabled);
                        writer.Write(sensorCode);
                        writer.Write(sensorName);
                        writer.Write(data.Sensor4_AlarmZoneMin);
                        writer.Write(data.Sensor4_AlarmZoneMax);
                    }
                    else if (typeof(T) == typeof(TrackerStructureSettingsLls))
                    {
                        var data = (TrackerStructureSettingsLls)(object)item;
                        // lls1
                        writer.Write(data.LevelThresholdLls1);
                        writer.Write(data.DrainThresholdLls1);
                        writer.Write(data.FillUpThresholdLls1);
                        writer.Write(data.CntEmptyLls1);
                        writer.Write(data.CntFullLls1);
                        writer.Write(data.MinLevelLls1);
                        writer.Write(data.MaxLevelLls1);
                        writer.Write(data.OutTypeLls1);
                        writer.Write(data.FlterTypeLls1);
                        writer.Write(data.AvarageLenghLls1);
                        writer.Write(data.MedianLenghtLls1);
                        writer.Write(data.QLls1);
                        writer.Write(data.RLls1);
                        writer.Write(data.ThermocompTypeLls1);
                        writer.Write(data.K1Lls1);
                        writer.Write(data.K2Lls1);
                        writer.Write(data.InterpolationTypeLls1);
                        writer.Write(data.WaterModeTypeLls1);
                        // lls2
                        writer.Write(data.LevelThresholdLls2);
                        writer.Write(data.DrainThresholdLls1);
                        writer.Write(data.FillUpThresholdLls2);
                        writer.Write(data.CntEmptyLls2);
                        writer.Write(data.CntFullLls2);
                        writer.Write(data.MinLevelLls2);
                        writer.Write(data.MaxLevelLls2);
                        writer.Write(data.OutTypeLls2);
                        writer.Write(data.FlterTypeLls2);
                        writer.Write(data.AvarageLenghLls2);
                        writer.Write(data.MedianLenghtLls2);
                        writer.Write(data.QLls2);
                        writer.Write(data.RLls2);
                        writer.Write(data.ThermocompTypeLls2);
                        writer.Write(data.K1Lls2);
                        writer.Write(data.K2Lls2);
                        writer.Write(data.InterpolationTypeLls2);
                        writer.Write(data.WaterModeTypeLls2);
                    }
                    else if (typeof(T) == typeof(TrackerStructureSettingsSms))
                    {
                        var data = (TrackerStructureSettingsSms)(object)item;
                        var phoneNumber = new byte[16];
                        data.Phone1Number.CopyTo(phoneNumber, 0);
                        writer.Write(phoneNumber);
                        writer.Write(data.Phone1IsEnable);
                        phoneNumber = data.Phone2Number;
                        writer.Write(phoneNumber);
                        writer.Write(data.Phone2IsEnable);
                        writer.Write(data.EventsPhone1);
                        writer.Write(data.EventsPhone2);
                    }
                    else if (typeof(T) == typeof(TrackerStructureSettingsTrack))
                    {
                        var data = (TrackerStructureSettingsTrack)(object)item;
                        writer.Write(data.MaxDistance);
                        writer.Write(data.MaxHeading);
                        writer.Write(data.SendTimeStop);
                        writer.Write(data.SendTimeSleep);
                        writer.Write(data.AccelThresholdSleep);
                        writer.Write(data.StopToMoveSleep);
                        writer.Write(data.MoveToStopSleep);
                        writer.Write(data.MaxStendingTime);
                        writer.Write(data.MaxSpeep);
                        writer.Write(data.PosAccel);
                        writer.Write(data.NegAccel);
                        writer.Write(data.IgnType);
                        writer.Write(data.IgnThreshold);
                    }
                    else if (typeof(T) == typeof(TrackerStructureSleep))
                    {
                        var data = (TrackerStructureSleep)(object)item;
                        writer.Write(data.SleepIsEnabled);
                        writer.Write(data.SleepVoltageThreshold);
                    }
                    else
                    {
                        throw new InvalidDataException("Undefined type");
                    }
                }
                return stream.ToArray();
            }
        }
    }
}
