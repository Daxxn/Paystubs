using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Models
{
    public abstract class ModelBase
    {
        #region - Fields & Properties
        public Guid _Id { get; private set; } = Guid.NewGuid();
        #endregion

        #region - Constructors
        public ModelBase( ) { }
        #endregion

        #region - Methods

        #endregion

        #region - Full Properties

        #endregion
    }
}
