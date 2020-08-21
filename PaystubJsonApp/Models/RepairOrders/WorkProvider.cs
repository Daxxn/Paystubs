using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Models.ReapirOrders
{
    public class WorkProvider
    {
        #region - Fields & Properties
        private static Dictionary<int, WorkItem> _workContainer { get; set; } =
            Debug.Debug.Instance.Active
                ? new Dictionary<int, WorkItem>
                {
                    { 001, new WorkItem{ Name="Test 1" } },
                    { 002, new WorkItem{ Name="Test 2" } },
                    { 003, new WorkItem{ Name="Test 3" } },
                }
                : new Dictionary<int, WorkItem>();

        public ObservableCollection<WorkItem> WorkData { get; private set; }
        #endregion

        #region - Constructors
        public WorkProvider( )
        {
            if ( AppSettings.Default.UseDefaultWorkItems )
            {
                // Build default WorkItems...
            }

            if ( Debug.Debug.Instance.Active )
            {
                WorkData = new ObservableCollection<WorkItem>(WorkContainer.Values);
            }
        }
        #endregion

        #region - Methods
        public void AddWorkItem( WorkItem item )
        {
            bool success = WorkContainer.TryGetValue(item.WorkIdNumber, out WorkItem foundItem);
            if ( !success )
            {
                WorkContainer.Add(item.WorkIdNumber, item);
            }
            else
            {
                WorkContainer[ item.WorkIdNumber ] = item;
            }
            WorkData.Add(WorkContainer[ item.WorkIdNumber ]);
        }

        public void RemoveWorkItem( WorkItem item, bool clearBase )
        {
            if ( clearBase )
            {
                bool success = WorkContainer.Remove(item.WorkIdNumber);
                if ( !success )
                {
                    Debug.Debug.Instance.Post(
                        "Error",
                        "Work item not found in WorkContainer during removal.",
                        new string[] { item.ToString() }
                    );
                }
            }

            WorkData.Remove(item);
        }
        #endregion

        #region - Full Properties
        public static Dictionary<int, WorkItem> WorkContainer
        {
            get => _workContainer;
            set => _workContainer = value;
        }
        #endregion
    }
}
