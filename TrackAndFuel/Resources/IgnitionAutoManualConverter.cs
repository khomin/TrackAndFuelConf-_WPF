using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static TrackAndFuel.ViewModel.SettingsViewModel;

namespace TrackAndFuel.Resources
{
    public class IgnitionAutoManualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i = (IgnitionDetectionType)value;
            return i == IgnitionDetectionType.AutoByPower;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
