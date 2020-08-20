using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Models.ReapirOrders
{
    public class WorkItem : ModelBase
    {
        #region - Fields & Properties
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double FlatRateTime { get; set; }
        public int WorkIdNumber { get; set; }
        #endregion

        #region - Constructors
        public WorkItem( ) { }
        #endregion

        #region - Methods
        public override string ToString( )
        {
            return $"{Name} : {(Description.Length > 10 ? Description.Substring(0, 10) : Description)}... , {FlatRateTime:G6)}";
        }
        #endregion

        #region - Full Properties

        #endregion
    }
}
