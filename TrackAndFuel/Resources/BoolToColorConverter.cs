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
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = Brushes.Red;
            if (value.GetType() == typeof(int))
            {
                if ((int)value == 0)
                {
                    color = Brushes.Red;
                }
                else 
                {
                    color = Brushes.Green;
                }
            }
            else
            {
                if (value.GetType() == typeof(bool))
                {
                    if ((bool)value == true)
                    {
                        color = Brushes.Red;
                    }
                    else
                    {
                        color = Brushes.Green;
                    }
                }
                else 
                {
                    Console.WriteLine("BoolToColorConveter: invalid type");
                }
            }
            return color;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
