using PaystubJsonApp.Models.Work;
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
        /// <summary>
        /// WorkItem Used in the RepairOrder List
        /// </summary>
        public ObservableCollection<WorkItem> Work
        {
            get => ( ObservableCollection<WorkItem> )GetValue(WorkProperty);
            set => SetValue(WorkProperty, value);
        }

        // Using a DependencyProperty as the backing store for Work.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WorkProperty =
            DependencyProperty.Register(
                "Work",
                typeof(ObservableCollection<WorkItem>),
                typeof(WorkItemControl),
                new PropertyMetadata(null)
            );

        /// <summary>
        /// The Parent ID of the WorkItem.
        /// </summary>
        public Guid RepairOrderID
        {
            get => ( Guid )GetValue(RepairOrderIDProperty);
            set => SetValue(RepairOrderIDProperty, value);
        }

        // Using a DependencyProperty as the backing store for RepairOrderID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RepairOrderIDProperty =
            DependencyProperty.Register(
                "RepairOrderID",
                typeof(Guid),
                typeof(WorkItemControl),
                new PropertyMetadata(null)
            );

        /// <summary>
        /// View Model of control.
        /// </summary>
        public RepairOrderViewModel VM
        {
            get => ( RepairOrderViewModel )GetValue(VMProperty);
            set => SetValue(VMProperty, value);
        }

        // Using a DependencyProperty as the backing store for VM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VMProperty =
            DependencyProperty.Register(
                "VM",
                typeof(RepairOrderViewModel),
                typeof(WorkItemControl),
                new PropertyMetadata(null)
            );

        public WorkItemControl( ) => InitializeComponent();

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            try
            {
                Button button = sender as Button;
                WorkItem workItem = button.DataContext as WorkItem;
                VM.RemoveWorkFromRepairOrder(
                    workItem,
                    VM.RepairOrderCollection.RepairOrders.FirstOrDefault(
                        ro => RepairOrderID == ro._Id
                    )
                );
            }
            catch ( Exception exe )
            {
                Debug.Debug.Instance.Post(
                    "Error",
                    $"Delete button error: {exe.Message}",
                    new string[] { sender.ToString(), e.RoutedEvent.ToString() }
                );
            }
        }
    }
}
