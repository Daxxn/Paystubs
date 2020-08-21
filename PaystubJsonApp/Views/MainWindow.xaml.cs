using PaystubJsonApp.ViewModels;

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

namespace PaystubJsonApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow( MainViewModel vm )
        {
            InitializeComponent();
            DataContext = vm;
            //InitializeEvents(vm);
            PaystubControl.Content = new PaystubView(vm.PaystubVM);
            RepairOrderControl.Content = new RepairOrderView(vm.RepairOrderVM);
        }

        //private void InitializeEvents( MainViewModel vm )
        //{
        //}

        private void TabControl_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
        }
    }
}
