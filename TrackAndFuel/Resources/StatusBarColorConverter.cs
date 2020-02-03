using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TrackAndFuel.Resources
{
    public class StatusBarColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return new SolidColorBrush(Color.FromArgb(255, (byte)0, (byte)0x97, (byte)0x2d));
            }
            else
            {
                return new SolidColorBrush(Color.FromArgb(255, (byte)0x40, (byte)0x6d, (byte)0x9f));
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}

