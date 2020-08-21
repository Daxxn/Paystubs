using PaystubJsonApp.Models.Paystubs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.ViewModels.Events
{
    public class AddPaystubsEventArgs
    {
        #region - Fields & Properties
        public IEnumerable<PaystubModel> NewPaystubs { get; private set; }
        #endregion

        #region - Constructors
        public AddPaystubsEventArgs( IEnumerable<PaystubModel> newPaystubs ) => NewPaystubs = newPaystubs;
        #endregion

        #region - Methods

        #endregion

        #region - Full Properties

        #endregion
    }
}
