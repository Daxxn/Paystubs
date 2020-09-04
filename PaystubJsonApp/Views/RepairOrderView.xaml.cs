using PaystubJsonApp.Models.RepairOrders;
using PaystubJsonApp.Models.Work;
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
        public RepairOrderView( RepairOrderViewModel vm )
        {
            InitializeComponent();
            DataContext = vm;
            InitializeEvents(vm);
        }

        private void InitializeEvents( RepairOrderViewModel vm )
        {
            AddRepairOrdersButton.Click += vm.AddRepairOrder;
            SaveFileButton.Click += vm.SaveFile;
            OpenFileButton.Click += vm.OpenFile;
            OpenSavePath.Click += vm.OpenSavePath;
        }

        private void ComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            RepairOrderViewModel vm = DataContext as RepairOrderViewModel;

            try
            {
                ComboBox CBItem = sender as ComboBox;
                RepairOrder repairOrder = CBItem.DataContext as RepairOrder;
                vm.AddWorkToRepairOrder(( WorkItem )e.AddedItems[ 0 ], repairOrder);
            }
            catch ( Exception exe )
            {
                Debug.Debug.Instance.Post(
                    "Error",
                    $"Work Selection Error: {exe.Message}",
                    new string[] { sender.ToString(), e.AddedItems.Count.ToString() }
                );
            }

        }
    }
}
