using PaystubJsonApp.Models.ReapirOrders;
using PaystubJsonApp.Models.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Models.RepairOrders
{
    public class RepairOrder : ModelBase
    {
        #region - Fields & Properties
        public int RONumber { get; set; }
        public DateTime Date { get; set; }
        public WorkProvider Work { get; set; }
        #endregion

        #region - Constructors
        public RepairOrder( ) { }
        #endregion

        #region - Methods

        #endregion

        #region - Full Properties
        public double TotalTime
        {
            get
            {
                return Work is null || Work.WorkItems is null
                    ? 0
                    : Work.WorkItems.Sum(item => item.FlatRateTime);
            }
        }
        #endregion
    }
}
