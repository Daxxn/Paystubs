using PaystubJsonApp.Models.ReapirOrders;
using PaystubJsonApp.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for WorkItemControl.xaml
    /// </summary>
    public partial class WorkItemControl : UserControl
    {


        public ObservableCollection<WorkItem> Work
        {
            get { return (ObservableCollection<WorkItem>)GetValue(WorkProperty); }
            set { SetValue(WorkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Work.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WorkProperty =
            DependencyProperty.Register("Work", typeof(ObservableCollection<WorkItem>), typeof(WorkItemControl), new PropertyMetadata(null));



        public Guid RepairOrderID
        {
            get { return (Guid)GetValue(RepairOrderIDProperty); }
            set { SetValue(RepairOrderIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RepairOrderID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RepairOrderIDProperty =
            DependencyProperty.Register("RepairOrderID", typeof(Guid), typeof(WorkItemControl), new PropertyMetadata(null));





        public RepairOrderViewModel VM
        {
            get { return (RepairOrderViewModel)GetValue(VMProperty); }
            set { SetValue(VMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VMProperty =
            DependencyProperty.Register("VM", typeof(RepairOrderViewModel), typeof(WorkItemControl), new PropertyMetadata(null));




        public WorkItemControl( )
        {
            InitializeComponent();
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            var button = sender as Button;
            var workItem = button.DataContext as WorkItem;
            VM.RemoveWorkFromRepairOrder(
                workItem,
                VM.RepairOrderCollection.RepairOrders.FirstOrDefault(
                    ro => RepairOrderID == ro._Id
                )
            );
        }
    }
}
