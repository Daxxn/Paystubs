using PaystubJsonApp.Exceptions;
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
        public WorkCollection WorkCollection { get; set; } = WorkCollection.Instance;

        public ObservableCollection<WorkItem> WorkItems { get; set; }
        #endregion

        #region - Constructors
        public WorkProvider( )
        {
            WorkItems = new ObservableCollection<WorkItem>();
        }
        public WorkProvider( IEnumerable<WorkItem> workItems )
        {
            WorkItems = new ObservableCollection<WorkItem>(workItems);
        }
        #endregion

        #region - Methods
        public void AddWork( WorkItem item )
        {
            if ( !WorkCollection.Data.Contains(item) )
            {
                throw new UnknownWorkItemException(item);
            }

            if ( !WorkItems.Contains(item) )
            {
                WorkItems.Add(item);
            }
        }

        public void RemoveWork( WorkItem item )
        {
            WorkItems.Remove(item);
        }
        #endregion

        #region - Full Properties
        #endregion
    }
}
