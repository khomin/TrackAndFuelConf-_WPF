using System;
using Tako.CRC;

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

        public override TrackerDataResult Parse()
        {
            TrackerDataResult result = null;
            CRCManager manager = new CRCManager() { DataFormat = EnumOriginalDataFormat.HEX };
            var provider = manager.CreateCRCProvider(EnumCRCProvider.CRC8DALLASMAXIM);
            try
            {
                if(data.Length != 0)
                {
                    if (provider.GetCRC(data).CrcDecimal == data[0])
                    {
                        result = new TrackerDataResult();
                        
                        result.AddValue(typeof(int), 10);
                    }
                }
            }
            catch (Exception) {}

            return result;
        }

        public override TrackerDataResult Parse(byte[] data)
        {
            this.data = data;
            return Parse();
        }
    }
}
