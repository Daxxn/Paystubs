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
    /// Interaction logic for FileControl.xaml
    /// </summary>
    public partial class FileControl : UserControl
    {

        public string SavePath
        {
            get { return ( string )GetValue(SavePathProperty); }
            set { SetValue(SavePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SavePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SavePathProperty =
            DependencyProperty.Register("SavePath", typeof(string), typeof(FileControl), new PropertyMetadata(""));


        public FileControl( )
        {
            InitializeComponent();
            DataContext = new FileControlViewModel();
        }
    }
}
