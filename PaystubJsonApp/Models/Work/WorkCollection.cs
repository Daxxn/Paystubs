using PaystubJsonApp.Models.ReapirOrders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Models.Work
{
    public class WorkCollection : ModelBase
    {
        #region - Fields & Properties
        private static WorkCollection _instance;

        private ObservableCollection<WorkItem> _data;

        private Func<ObservableCollection<WorkItem>, int, bool> IDSearch =
            ( data, id ) => data.Any(x => x.WorkIdNumber == id);

        private Func<ObservableCollection<WorkItem>, int, WorkItem> Find =
            ( data, id ) => data.First(x => x.WorkIdNumber == id);
        #endregion

        #region - Constructors
        private WorkCollection( )
        {
            _data = new ObservableCollection<WorkItem>();
        }
        private WorkCollection( IEnumerable<WorkItem> data )
        {
            _data = new ObservableCollection<WorkItem>(data);
        }
        #endregion

        #region - Methods
        public static WorkCollection New( IEnumerable<WorkItem> data )
        {
            _instance = new WorkCollection(data);
            return Instance;
        }

        public bool Add( int id, WorkItem item )
        {
            bool result = IDSearch(_data, id);
            if (! result )
            {
                _data.Add(item);
            }
            return result;
        }

        public bool Remove( int id )
        {
            return _data.Remove(Find(_data, id));
        }

        public void Update( WorkItem item )
        {
            var found = Find(_data, item.WorkIdNumber);
            if ( found != null )
            {
                found = item;
            }
        }
        #endregion

        #region - Full Properties
        public ObservableCollection<WorkItem> Data
        {
            get => _data;
        }

        public WorkItem this[int id ]
        {
            get
            {
                return Find(_data, id);
            }
        }

        public static WorkCollection Instance
        {
            get
            {
                if ( _instance is null )
                {
                    _instance = new WorkCollection();
                }
                return _instance;
            }
        }
        #endregion
    }
}
