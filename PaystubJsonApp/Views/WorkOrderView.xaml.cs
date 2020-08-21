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
    /// Interaction logic for WorkOrderView.xaml
    /// </summary>
    public partial class WorkOrderView : UserControl
    {
        public WorkOrderView( WorkOrderViewModel vm )
        {
            InitializeComponent();
            DataContext = vm;
            InitializeEvents(vm);
        }

        private void InitializeEvents( WorkOrderViewModel vm )
        {
            WorkOrdersDataGrid.CellEditEnding += vm.EditEnding;
            AddWorkItemButton.Click += vm.AddWorkItem;
        }
    }
}
