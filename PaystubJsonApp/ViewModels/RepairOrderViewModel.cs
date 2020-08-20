using PaystubJsonApp.Models.ReapirOrders;
using PaystubJsonApp.Models.RepairOrders;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PaystubJsonApp.ViewModels
{
    public class RepairOrderViewModel : ViewModelBase
    {
        #region - Fields & Properties
        private RepairOrderCollection _repairOrderCollection;
        private RepairOrder _newRepairOrder;
        #endregion

        #region - Constructors
        public RepairOrderViewModel( )
        {
            NewRepairOrder = new RepairOrder();
            BuildDebugInstanceData();
        }
        #endregion

        #region - Methods
        public void AddRepairOrder( object sender, EventArgs e )
        {
            RepairOrderCollection.RepairOrders.Add(new RepairOrder()
            {
                RONumber = 111,
                Date = new DateTime(2020, 5, 5)
            });;
        }

        private void BuildDebugInstanceData( )
        {
            if (Debug.Debug.Instance.DefaultValues)
            {
                RepairOrderCollection = new RepairOrderCollection()
                {
                    RepairOrders = new ObservableCollection<RepairOrder>
                    {
                        new RepairOrder
                        {
                            RONumber = 111111,
                            Date = DateTime.Now,
                            Work = new WorkProvider()
                        },
                        new RepairOrder
                        {
                            RONumber = 222222,
                            Date = DateTime.Now,
                            Work = new WorkProvider()
                        }
                    }
                };
            }
        }
        #endregion

        #region - Full Properties
        public RepairOrderCollection RepairOrderCollection
        {
            get { return _repairOrderCollection; }
            set
            {
                _repairOrderCollection = value;
                NotifyOfPropertyChange(nameof(RepairOrderCollection));
                NotifyOfPropertyChange(nameof(NoROs));
            }
        }

        public RepairOrder NewRepairOrder
        {
            get { return _newRepairOrder; }
            set
            {
                _newRepairOrder = value;
                NotifyOfPropertyChange(nameof(NewRepairOrder));
            }
        }

        public Visibility NoROs
        {
            get
            {
                if (RepairOrderCollection != null)
                {
                    if (RepairOrderCollection.RepairOrders != null)
                    {
                        return RepairOrderCollection.RepairOrders.Count <= 0 ? Visibility.Visible : Visibility.Hidden;
                    }
                }
                return Visibility.Visible;
            }
        }
        #endregion
    }
}
