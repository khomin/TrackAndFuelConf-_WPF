using System;
using System.Collections.Generic;

namespace trackerWpfConf.Instrumentals
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

        public override (TrackerTypeData.TypePacketData type, List<DataItemParam> data) Parse()
        {
            TrackerTypeData.TypePacketData typePacket = TrackerTypeData.TypePacketData.Request;
            List<DataItemParam> result = new List<DataItemParam>();
            try
            {
                if (data.Length != 0)
                {
                    byte[] crcArray = new byte[data.Length - 1];
                    Array.Copy(data, 0, crcArray, 0, crcArray.Length);
                    if (Crc8Calc.ComputeChecksum(crcArray) == data[data.Length-1])
                    {
                        typePacket = (TrackerTypeData.TypePacketData)(int)data[(int)TrackerTypeData.PacketField.Header];
                        result = ParseField(data, (int)TrackerTypeData.PacketField.ParamsCount + 1, data[(int)TrackerTypeData.PacketField.ParamsCount]);
                    }
                }
            }
            catch (Exception) { }

            return (typePacket, result);
        }

        public override (TrackerTypeData.TypePacketData type, List<DataItemParam> data) Parse(byte[] data)
        {
            this.data = data;
            return Parse();
        }

        private List<DataItemParam> ParseField(byte[] data, int beginIndex, int paramsCount)
        {
            List<DataItemParam> list = new List<DataItemParam>();

            while (paramsCount != 0)
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
                paramsCount -= 1;
                list.Add(dataField);
            }
            return list;
        }
    }
}
