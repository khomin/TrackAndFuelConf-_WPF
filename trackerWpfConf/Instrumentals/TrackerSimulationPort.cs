using System;
using System.Collections.Generic;

namespace trackerWpfConf.Instrumentals
{
    class TrackerSimulationPort : TrackerDataPortAbstract
    {
        private Action disonnect;
        public TrackerSimulationPort(Action<List<int>> callbackData, Action disonnect) 
        {
            System.Timers.Timer testTimer = new System.Timers.Timer(2000);
            testTimer.AutoReset = true;
            testTimer.Enabled = true;
            testTimer.Elapsed += (sourse, evendt) =>
            {
                /* status */
                byte[] test = { 0x24, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x16, 0x00, 0x44, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x20, 0x74, 0x61, 0x73, 0x6B, 0x20, 0x77, 0x6F, 0x72, 0x6B, 0x69, 0x6E, 0x67, 0x0D, 0x0A, 0xB3 };
                var newData = new List<int>(test.Length);
                for (int i = 0; i < test.Length; i++)
                {
                    newData.Add(test[i]);
                }
                callbackData(newData);
            };
            this.disonnect = disonnect;
        }

        public override void Close()
        {
            if (disonnect != null) 
            {
                disonnect.Invoke();
            }
        }

        public override bool Open(Dictionary<string, object> property, Action<List<int>> callBack)
        {
            throw new NotImplementedException();
        }
    }
}
