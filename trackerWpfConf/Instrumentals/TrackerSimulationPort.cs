using System;
using System.Collections.Generic;

namespace trackerWpfConf.Instrumentals
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
                updateDataCallback.Invoke(GetNextStatusPacket());

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
            byte[] test = { 0x24, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x16, 0x00, 0x44, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x20, 0x74, 0x61, 0x73, 0x6B, 0x20, 0x77, 0x6F, 0x72, 0x6B, 0x69, 0x6E, 0x67, 0x0D, 0x0A, 0xB3 };
            var newData = new List<int>(test.Length);
            for (int i = 0; i < test.Length; i++)
            {
                newData.Add(test[i]);
            }
            return newData;
        }
    }
}
