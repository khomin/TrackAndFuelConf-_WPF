using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static TrackAndFuel.ViewModel.InputItemSettingsModel;

namespace TrackAndFuel.Resources
{
    public class InputRoleDiscreateOrAnalogConverter : IValueConverter 
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = (PortRole)value;
            var visible = Visibility.Collapsed;
            switch (d)
            {
                case PortRole.notUsed: break;
                case PortRole.discrete:
                    visible = Visibility.Visible;
                        break;
                case PortRole.frequencySensor:
                    break;
                case PortRole.voltageMeasurement:
                    break;
            }
            return visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
