using PaystubJsonApp.Models.ReapirOrders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PaystubJsonApp.ViewModels
{
    public class WorkOrderViewModel : ViewModelBase
    {
        #region - Fields & Properties
        private ObservableCollection<WorkItem> _allWorkData
            = new ObservableCollection<WorkItem>(
                WorkProvider.WorkContainer.Values
            );

        private WorkItem _newWorkItem = new WorkItem();
        #endregion

        #region - Constructors
        public WorkOrderViewModel( ) { }
        #endregion

        #region - Methods
        public void EditEnding( object sender, DataGridCellEditEndingEventArgs e )
        {
            var element = e.EditingElement as TextBox;
            var data = element.DataContext as WorkItem;
            WorkProvider.WorkContainer[ data.WorkIdNumber ] = data;
        }

        public void AddWorkItem( object sender, EventArgs e )
        {
            if ( WorkProvider.AddNewWorkItem(NewWorkItem) )
            {
                AllWorkData.Add(NewWorkItem);
            }
        }
        #endregion

        #region - Full Properties
        public ObservableCollection<WorkItem> AllWorkData
        {
            get { return _allWorkData; }
            set
            {
                _allWorkData = value;
                NotifyOfPropertyChange(nameof(WorkProvider));
            }
        }

        public WorkItem NewWorkItem
        {
            get { return _newWorkItem; }
            set
            {
                _newWorkItem = value;
                NotifyOfPropertyChange(nameof(NewWorkItem));
            }
        }
        #endregion
    }
}
