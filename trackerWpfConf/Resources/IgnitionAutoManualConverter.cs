using System;
using System.Globalization;
using System.Windows.Data;
using static trackerWpfConf.ViewModel.SettingsViewModel;

namespace trackerWpfConf.Resources
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
