using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PaystubJsonApp.ValueConverters
{
    public class ColorConverter : IValueConverter
    {
        public static Color Red { get; } = new Color { R = 200, G = 0, B = 0 };
        public static Color Green { get; } = new Color { R = 0, G = 200, B = 0 };
        public static SolidColorBrush TrueColor { get; } = new SolidColorBrush(Green);
        public static SolidColorBrush FalseColor { get; } = new SolidColorBrush(Red);
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            bool val = ( bool )value;
            return val ? TrueColor : FalseColor;
        }
        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var val = value as SolidColorBrush;
            return val == TrueColor ? true : false;
        }
    }
}
