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
