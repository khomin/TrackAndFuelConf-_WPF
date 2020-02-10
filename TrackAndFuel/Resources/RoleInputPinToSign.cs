using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static TrackAndFuel.ViewModel.InputItemSettingsModel;

namespace TrackAndFuel.Resources
{
    class RoleInputPinToSign : IValueConverter
        {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = (PortRole)value;
            string sign = "";
            switch (d)
            {
                case PortRole.notUsed:
                case PortRole.discrete:
                    sign = "V"; 
                    break;
                case PortRole.frequencySensor:
                    sign = "Hz";
                    break;
                case PortRole.voltageMeasurement:
                    sign = "V";
                    break;
            }

            return sign;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
