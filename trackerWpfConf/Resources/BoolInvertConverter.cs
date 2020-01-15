using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace trackerWpfConf.Resources
{
    public class BoolInvertionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // Do the conversion from bool to visibility
            if (value != null && value is bool)
            {
                return !((bool)value);
            }

            return true;
        }
        public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            // Do the conversion from visibility to bool
            return Convert(value, targetType, parameter, culture);
        }
    }
}