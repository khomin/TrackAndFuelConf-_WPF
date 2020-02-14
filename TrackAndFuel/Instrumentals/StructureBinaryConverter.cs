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
                    //var data = item as TrackerStructureSettingsConnection;
                    ///* first */
                    //data.ProtocolType_0 = reader.ReadBytes(1);
                    //data.ConnectAddr_0 = reader.ReadBytes(64);
                    //data.ConnectPassword_0 = reader.ReadBytes(16);
                    //data.ConnectPort_0 = reader.ReadBytes(2);
                    //data.ConnectUseCompression_0 = reader.ReadBytes(1);
                    //data.ConnectPeriodOfPingMessage_0 = reader.ReadBytes(4);
                    //data.ConnectDelayBeforeNextConnecting_0 = reader.ReadBytes(4);
                    //data.ConnectSendingPeropdDuringParking_0 = reader.ReadBytes(4);
                    //data.ConnectSendingPeropdInSleepMode_0 = reader.ReadBytes(4);
                    //data.AdditionParams_0 = reader.ReadBytes(4);
                    ///* second */
                    //data.ProtocolType_1 = reader.ReadBytes(1);
                    //data.ConnectAddr_1= reader.ReadBytes(64);
                    //data.ConnectPassword_1 = reader.ReadBytes(16);
                    //data.ConnectPort_1 = reader.ReadBytes(2);
                    //data.ConnectUseCompression_1 = reader.ReadBytes(1);
                    //data.ConnectPeriodOfPingMessage_1 = reader.ReadBytes(4);
                    //data.ConnectDelayBeforeNextConnecting_1 = reader.ReadBytes(4);
                    //data.ConnectSendingPeropdDuringParking_1 = reader.ReadBytes(4);
                    //data.ConnectSendingPeropdInSleepMode_1 = reader.ReadBytes(4);
                    //data.AdditionParams_1 = reader.ReadBytes(4);

                }
                else if (typeof(T) == typeof(TrackerStructureSettingsOneWire))
                {

                }
                else if (typeof(T) == typeof(TrackerStructureSettingsLls))
                {

                }
                else if (typeof(T) == typeof(TrackerStructureSettingsSms))
                {

                }
                else if (typeof(T) == typeof(TrackerStructureSettingsTrack))
                {

                }
                else if (typeof(T) == typeof(TrackerStructureSleep)) 
                {
                
                }
                else
                {
                    throw new InvalidDataException("undefined type");
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
                        var data = item as TrackerStructureGsm;
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
                }
                return stream.ToArray();
            }
        }
    }
}
