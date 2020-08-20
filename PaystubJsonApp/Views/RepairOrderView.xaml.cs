using PaystubJsonApp.Models.ReapirOrders;
using PaystubJsonApp.Models.RepairOrders;
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
    /// Interaction logic for RepairOrderView.xaml
    /// </summary>
    public partial class RepairOrderView : UserControl
    {
        public RepairOrderViewModel VM { get; private set; }
        public RepairOrderView( RepairOrderViewModel vm )
        {
            InitializeComponent();
            DataContext = vm;
            VM = vm;
            InitializeEvents(vm);
        }

        private void InitializeEvents( RepairOrderViewModel vm )
        {
            AddRepairOrdersButton.Click += vm.AddRepairOrder;
        }

        private void ComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            var vm = DataContext as RepairOrderViewModel;

            try
            {
                var CBItem = sender as ComboBox;
                var repairOrder = CBItem.DataContext as RepairOrder;
                vm.AddWorkToRepairOrder((WorkItem)e.AddedItems[ 0 ], repairOrder);
            }
            catch (Exception exe)
            {
                Debug.Debug.Instance.Post(
                    "Error",
                    $"Work Selection Error: {exe.Message}",
                    new string[] { sender.ToString(), e.AddedItems.Count.ToString() }
                );
            }
            
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            var vm = DataContext as RepairOrderViewModel;
            var button = sender as Button;
            var workItem = button.DataContext as WorkItem;
            //vm.RemoveWorkFromRepairOrder(workItem);
        }
    }
}
