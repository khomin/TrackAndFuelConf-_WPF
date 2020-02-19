using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.Instrumentals.Tracker
{
    class TrackerCommandController
    {
        private TrackerDataToView trackerDataToView = null;
        private Action<string> errorHandler = null;
        public TrackerCommandController(TrackerDataToView trackerDataToView, Action<string> errorHandler) 
        {
            this.trackerDataToView = trackerDataToView;
            this.errorHandler = errorHandler;
        }

        private void DataToView() 
        {
            
        }
    }
}
