using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaystubJsonApp.Controls
{
    /// <summary>
    /// Interaction logic for CalcOutput.xaml
    /// </summary>
    public partial class CalcOutput : UserControl
    {
        public string Title
        {
            get { return ( string )GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CalcOutput), new PropertyMetadata(null));

        public string Gross
        {
            get { return ( string )GetValue(GrossProperty); }
            set { SetValue(GrossProperty, value); }
        }

        public static readonly DependencyProperty GrossProperty =
            DependencyProperty.Register("Gross", typeof(string), typeof(CalcOutput), new PropertyMetadata(null));

        public string Net
        {
            get { return ( string )GetValue(NetProperty); }
            set { SetValue(NetProperty, value); }
        }

        public static readonly DependencyProperty NetProperty =
            DependencyProperty.Register("Net", typeof(string), typeof(CalcOutput), new PropertyMetadata(null));

        public string Hours
        {
            get { return ( string )GetValue(HoursProperty); }
            set { SetValue(HoursProperty, value); }
        }

        public static readonly DependencyProperty HoursProperty =
            DependencyProperty.Register("Hours", typeof(string), typeof(CalcOutput), new PropertyMetadata(null));

        public string Flatrate
        {
            get { return ( string )GetValue(FlatrateProperty); }
            set { SetValue(FlatrateProperty, value); }
        }

        public static readonly DependencyProperty FlatrateProperty =
            DependencyProperty.Register("Flatrate", typeof(string), typeof(CalcOutput), new PropertyMetadata(null));


        public CalcOutput( )
        {
            InitializeComponent();
        }
    }
}
