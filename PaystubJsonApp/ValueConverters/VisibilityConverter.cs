using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PaystubJsonApp.ValueConverters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var val = (bool)value;
            return val ? Visibility.Visible : Visibility.Hidden;
        }
        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var vis = (Visibility)value;
            return vis == Visibility.Visible ? true : false;
        }
    }
}
