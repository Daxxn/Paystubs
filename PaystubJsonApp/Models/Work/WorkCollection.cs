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
        //private Dictionary<uint, WorkItem> _dictionary;
        private ObservableCollection<WorkItem> _data;

        private Func<ObservableCollection<WorkItem>, int, bool> IDSearch =
            ( data, id ) => data.Any(x => x.WorkIdNumber == id);

        private Func<ObservableCollection<WorkItem>, int, WorkItem> Find =
            ( data, id ) => data.First(x => x.WorkIdNumber == id);
        #endregion

        #region - Constructors
        public WorkCollection( )
        {
            _data = new ObservableCollection<WorkItem>();
        }
        public WorkCollection( IEnumerable<WorkItem> data )
        {
            _data = new ObservableCollection<WorkItem>(data);
        }
        #endregion

        #region - Methods
        public bool Add( int id, WorkItem item )
        {
            //if ( _data.First(x => x.WorkIdNumber == id) is null )
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

            //var index = _data.IndexOf(item);
            //if ( index >= 0 )
            //{
            //    _data.ElementAt(index) = item;
            //}
        }
        #endregion

        #region - Full Properties
        //public Dictionary<uint, WorkItem> Dictionary
        //{
        //    get { return _dictionary; }
        //    set
        //    {
        //        _dictionary = value;
        //    }
        //}

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
        #endregion

        //OLD
        //#region - Fields & Properties
        //public ObservableCollection<WorkItem> WorkData { get; private set; }
        //#endregion

        //#region - Constructors
        //public WorkCollection( ) { }
        //public WorkCollection( IEnumerable<WorkItem> workData )
        //{
        //    WorkData = new ObservableCollection<WorkItem>(workData);
        //}

        //public event NotifyCollectionChangedEventHandler CollectionChanged;
        //#endregion

        //#region - Methods
        //public void AddWorkItem( WorkItem item )
        //{
        //    if ( true )
        //    {

        //    }
        //}
        //#endregion

        //#region - Full Properties

        //#endregion
    }
}
