using PaystubJsonApp.Models.ReapirOrders;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Models.Work
{
    public class WorkProvider
    {
        #region - Fields & Properties
        private static WorkProvider _instance;

        public WorkCollection WorkInstances { get; private set; }
        #endregion

        #region - Constructors
        private WorkProvider( ) { }
        #endregion

        #region - Methods
        public static void Initialize( )
        {
            Instance = new WorkProvider();
        }

        public static void Initialize( IEnumerable<WorkItem> newData )
        {
            Instance = new WorkProvider()
            {
                WorkInstances = new WorkCollection(newData)
            };
        }
        #endregion

        #region - Full Properties
        public static WorkProvider Instance
        {
            get
            {
                if ( _instance is null )
                {
                    _instance = new WorkProvider();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        #endregion
    }
}
