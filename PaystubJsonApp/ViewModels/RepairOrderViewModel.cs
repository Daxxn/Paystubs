using PaystubJsonApp.Models.ReapirOrders;
using PaystubJsonApp.Models.RepairOrders;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.ViewModels
{
    public class RepairOrderViewModel : ViewModelBase
    {
        #region - Fields & Properties
        private RepairOrderCollection _repairOrderCollection;
        #endregion

        #region - Constructors
        public RepairOrderViewModel( )
        {
            BuildDebugInstanceData();
        }
        #endregion

        #region - Methods
        private void BuildDebugInstanceData( )
        {
            if (Debug.Debug.Instance.Active)
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
            }
        }
        #endregion
    }
}
