using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Models.RepairOrders
{
    public class RepairOrderCollection : ModelBase
    {
        #region - Fields & Properties
        public ObservableCollection<RepairOrder> _repairOrders;
        #endregion

        #region - Constructors
        public RepairOrderCollection( ) { }
        #endregion

        #region - Methods
        public bool Contains( int roNumber )
        {
            foreach ( var ro in RepairOrders )
            {
                if ( ro.RONumber == roNumber )
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains( RepairOrder ro )
        {
            return RepairOrders.Contains(ro);
        }
        #endregion

        #region - Full Properties
        public ObservableCollection<RepairOrder> RepairOrders
        {
            get => _repairOrders;
            set => _repairOrders = value;
        }

        public double FlatrateTimeSum
        {
            get
            {
                return RepairOrders.Sum(ro => ro.TotalTime);
            }
        }
        #endregion
    }
}
