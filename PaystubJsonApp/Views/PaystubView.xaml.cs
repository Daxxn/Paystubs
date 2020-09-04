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
    /// Interaction logic for PaystubView.xaml
    /// </summary>
    public partial class PaystubView : UserControl
    {
        public PaystubView( PaystubViewModel vm )
        {
            InitializeComponent();
            DataContext = vm;
            InitializeEvents(vm);
            propCombo.ItemsSource = Enum.GetValues(typeof(Prop));
            if ( Debug.Debug.Instance.Active )
            {
                KeyDown += Debug.Debug.Instance.PostEvent;
                MouseDown += Debug.Debug.Instance.PostEvent;
                FocusManager.AddGotFocusHandler(this, Debug.Debug.Instance.PostEvent);
            }
        }

        private void InitializeEvents( PaystubViewModel vm )
        {
            test.Click += HandleAddViewOpen;
            OpenSavePath.Click += vm.HandleOpenSavePath;
            SaveFileButton.Click += vm.HandleSaveFile;
            OpenFileButton.Click += vm.HandleOpenFile;
            MainDataGrid.CellEditEnding += vm.HandleCellChanged;
            MainDataGrid.SelectionChanged += vm.Calculate;
            MainDataGrid.SelectedCellsChanged += vm.Calculate;
            MainDataGrid.CellEditEnding += vm.Calculate;
            MainDataGrid.Initialized += vm.Calculate;
            PaystubViewControl.Loaded += vm.Calculate;
            filterCombo.SelectionChanged += vm.FilterSelectionChanged;
            propCombo.SelectionChanged += vm.PropSelectionChanged;
        }

        private void HandleAddViewOpen( object sender, EventArgs e )
        {
            PaystubViewModel vm = DataContext as PaystubViewModel;
            AddViewModel addVm = new AddViewModel();
            addVm.AddNewPaystubsEvent += vm.AddNewPaystubs;
            AddView addView = new AddView(addVm);
            Debug.Debug.Instance.Post("Event", "Opening AddView");
            addView.ShowDialog();
        }
    }
}
