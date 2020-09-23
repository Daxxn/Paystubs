using Microsoft.Win32;

using PaystubJsonApp.FileControl;
using PaystubJsonApp.Models;
using PaystubJsonApp.ViewModels.Events;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PaystubJsonApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region - Fields & Properties
        private PaystubViewModel _paystubVM;
        private RepairOrderViewModel _repairOrderVM;
        private WorkOrderViewModel _workOrderVM;
        #endregion

        #region - Constructors
        public MainViewModel( )
        {
            PaystubVM = new PaystubViewModel();
            WorkOrderVM = new WorkOrderViewModel();
            RepairOrderVM = new RepairOrderViewModel();
        }
        #endregion

        #region - Methods
        public ViewModelBase GetVM( string vmName )
        {
            switch ( vmName )
            {
                case "Paystubs":
                    return PaystubVM;
                case "RepairOrders":
                    return RepairOrderVM;
                default:
                    throw new Exception();
            }
        }
        #endregion

        #region - Full Properties
        public PaystubViewModel PaystubVM
        {
            get => _paystubVM;
            set
            {
                _paystubVM = value;
                NotifyOfPropertyChange(nameof(PaystubVM));
            }
        }

        public RepairOrderViewModel RepairOrderVM
        {
            get => _repairOrderVM;
            set
            {
                _repairOrderVM = value;
                NotifyOfPropertyChange(nameof(RepairOrderVM));
            }
        }

        public WorkOrderViewModel WorkOrderVM
        {
            get { return _workOrderVM; }
            set
            {
                _workOrderVM = value;
                NotifyOfPropertyChange(nameof(WorkOrderVM));
            }
        }
        #endregion
    }
}
