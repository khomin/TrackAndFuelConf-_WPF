using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackAndFuel.Instrumentals
{
    class TrackerParserData : TrackerParserDataAbstract
    {
        public TrackerParserData()
        {

        }
        public TrackerParserData(byte[] data)
        {
            this.data = data;
        }

        public override ParserResult Parse()
        {
            ParserResult result = null;
            if (data.Length != 0)
            {
                byte[] crcArray = new byte[data.Length - 1];
                Array.Copy(data, 0, crcArray, 0, crcArray.Length);
                if (Crc8Calc.ComputeChecksum(crcArray) == data[data.Length - 1])
                {
                    var packet = (TrackerTypeData.TypePacketData)(int)data[(int)TrackerTypeData.PacketField.TypePacket];
                    var bodyData = ParseField(data, (int)TrackerTypeData.PacketField.TypeMessage);
                    result = new ParserResult(packet, bodyData);
                }
                else
                {
                    return null;
                }
            }
            return result;
        }
        public override ParserResult Parse(byte[] data)
        {
            this.data = data;
            return Parse();
        }

        public override ParserResult Parse(List<int> data)
        {
            //this.data = data.Select(i => BitConverter.GetBytes(i)).SelectMany(i => i).ToArray();

            this.data = new byte[data.Count];
            for (int i = 0; i < this.data.Length; i++)
            {
                this.data[i] = (byte)data[i];
            }
            return Parse();
        }

        public List<byte> addParam(DataItemParam data)
        {
            var res = new List<byte>();
            res.Add((byte)data.Key);
            if (data.Type == typeof(int))
            {
                res.Add((byte)TrackerTypeData.TypeParameter.ParamTypeInt);
                var bytearray = BitConverter.GetBytes((int)data.Data);
                res.Add(bytearray[0]);
                res.Add(bytearray[1]);
                res.Add(bytearray[2]);
                res.Add(bytearray[3]);
            }
            else if (data.Type == typeof(bool))
            {
                res.Add((byte)TrackerTypeData.TypeParameter.ParamTypeBool);
                res.Add((byte)data.Data);
            }
            else if (data.Type == typeof(string))
            {
                res.Add((byte)TrackerTypeData.TypeParameter.ParamTypeString);
                res.Add((byte)data.Data.ToString().Length);
                res.Add(0);
                foreach (var i in data.Data.ToString())
                {
                    res.Add((byte)i);
                }
            }
            else if (data.Type == typeof(float))
            {
                res.Add((byte)TrackerTypeData.TypeParameter.ParamTypeFloat);
                var bytearray = BitConverter.GetBytes((float)data.Data);
                res.Add(bytearray[0]);
                res.Add(bytearray[1]);
                res.Add(bytearray[2]);
                res.Add(bytearray[3]);
            }
            else if (data.Type == typeof(byte[]))
            {
                res.Add((byte)TrackerTypeData.TypeParameter.ParamTypeBinary);
                var i = (byte[])data.Data;
                res.Add((byte)i.Length);
                var d = (byte[])data.Data;
                res.Add((byte)d.Length);
            }
            return res;
        }

        private List<DataItemParam> ParseField(byte[] data, int beginIndex)
        {
            List<DataItemParam> list = new List<DataItemParam>();
            try
            {
                while (beginIndex < data.Length-1)
                {
                    DataItemParam dataField = new DataItemParam();
                    dataField.Key = (TrackerTypeData.KeyParameter)(int)data[beginIndex++];
                    int len;
                    var paramType = (TrackerTypeData.TypeParameter)(int)data[beginIndex];
                    beginIndex += 1;
                    switch (paramType)
                    {
                        case TrackerTypeData.TypeParameter.ParamTypeInt:
                            dataField.Type = typeof(int);
                            dataField.Data = BitConverter.ToInt32(data, beginIndex);
                            beginIndex += 4;
                            break;
                        case TrackerTypeData.TypeParameter.ParamTypeFloat:
                            dataField.Type = typeof(float);
                            dataField.Data = BitConverter.ToSingle(data, beginIndex);
                            beginIndex += 4;
                            break;
                        case TrackerTypeData.TypeParameter.ParamTypeString:
                            len = BitConverter.ToUInt16(data, beginIndex);
                            beginIndex += 2;
                            dataField.Data = System.Text.Encoding.UTF8.GetString(data, beginIndex, len);
                            beginIndex += len;
                            dataField.Type = typeof(string);
                            break;
                        case TrackerTypeData.TypeParameter.ParamTypeBool:
                            dataField.Type = typeof(bool);
                            dataField.Data = BitConverter.ToBoolean(data, beginIndex);
                            beginIndex++;
                            break;
                        case TrackerTypeData.TypeParameter.ParamTypeBinary:
                            dataField.Type = typeof(byte[]);
                            len = BitConverter.ToUInt16(data, beginIndex);
                            beginIndex += 2;
                            byte[] objData = new byte[len];
                            Array.Copy(data, beginIndex, objData, 0, len);
                            dataField.Data = objData;
                            beginIndex += len;
                            break;
                    }
                    list.Add(dataField);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return list;
        }
    }
}
