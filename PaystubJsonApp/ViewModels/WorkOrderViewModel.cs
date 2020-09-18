using PaystubJsonApp.FileControl;
using PaystubJsonApp.Models.Work;

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
        private WorkCollection _allWork;
        private WorkItem _selectedWorkItem;

        private ObservableCollection<WorkItem> _commonWork;
        private ObservableCollection<WorkItem> _favoritedWork;

        private int _newWorkID;

        private bool _overwriteFile;
        private string _savePath;
        private bool _notSaved;

        private bool _doesItemExist;

        private int _totalWork;
        #endregion

        #region - Constructors
        public WorkOrderViewModel( )
        {
            OverwriteFile = AppSettings.Default.OverwriteFile;
            AllWork = WorkCollection.Instance;
            CommonWork = new ObservableCollection<WorkItem>();
        }
        #endregion

        #region - Methods

        #region File Controlls
        public void HandleOpenSavePath( object sender, EventArgs e ) =>
            SavePath = new FileDialogController<WorkCollection>().
                SaveFileDialog(FileExtensionType.WorkOrder);

        public void HandleOpenFile( object sender, EventArgs e )
        {
            FileDialogController<WorkCollection> opener = new FileDialogController<WorkCollection>();
            (string newPath, WorkCollection newWork) = opener.OpenFile(NotSaved);
            if ( newPath != null && newWork != null )
            {
                SavePath = newPath;
                AllWork = newWork;
                TotalWork = AllWork.Data.Count;
            }
            NotSaved = false;
        }

        public void HandleSaveFile( object sender, EventArgs e )
        {
            FileDialogController<WorkCollection> saver = new FileDialogController<WorkCollection>(AllWork);
            saver.SaveFile(SavePath);
        }
        #endregion
        public void EditEnding( object sender, DataGridCellEditEndingEventArgs e )
        {
            var element = e.EditingElement as TextBox;
            if ( element is null )
            {
                Debug.Debug.Instance.Post("Error", "Element was null");
                return;
            }
            var data = element.DataContext as WorkItem;
            AllWork.Update(data);
            NotSaved = true;
            UpdateWorkLists(data);
        }

        public void AddWorkItem( object sender, EventArgs e )
        {
            var newWorkItem = new WorkItem
            {
                WorkIdNumber = NewWorkID,
            };
            DoesItemExist = AllWork.Add(NewWorkID, newWorkItem);

            if ( !DoesItemExist )
            {
                UpdateWorkLists(newWorkItem);
                TotalWork = AllWork.Data.Count;
            }
        }

        public void UpdateWorkLists( WorkItem work )
        {
            if ( CommonWork.Count > 10 )
            {
                CommonWork.RemoveAt(10 - 1);
            }
            CommonWork.Remove(work);
            CommonWork.Insert(0, work);

            FavoritedWork = new ObservableCollection<WorkItem>(AllWork.Data.Where(w => w.IsFavorited == true));
        }
        #endregion

        #region - Full Properties
        public WorkCollection AllWork
        {
            get { return _allWork; }
            set
            {
                _allWork = value;
                NotSaved = true;
                TotalWork = value.Data.Count;
                NotifyOfPropertyChange(nameof(AllWork));
            }
        }

        public WorkItem SelectedWorkItem
        {
            get { return _selectedWorkItem; }
            set
            {
                var prev = _selectedWorkItem;
                _selectedWorkItem = value;
                if ( prev != value )
                {
                    UpdateWorkLists(value);
                }
                NotifyOfPropertyChange(nameof(SelectedWorkItem));
            }
        }

        public int NewWorkID
        {
            get { return _newWorkID; }
            set
            {
                _newWorkID = value;
                NotifyOfPropertyChange(nameof(NewWorkID));
            }
        }

        public bool DoesItemExist
        {
            get { return _doesItemExist; }
            set
            {
                _doesItemExist = value;
                NotifyOfPropertyChange(nameof(DoesItemExist));
                NotifyOfPropertyChange(nameof(DoesItemExistInvert));
            }
        }

        public bool DoesItemExistInvert
        {
            get => !DoesItemExist;
        }

        public bool OverwriteFile
        {
            get { return _overwriteFile; }
            set
            {
                _overwriteFile = value;
                NotifyOfPropertyChange(nameof(OverwriteFile));
            }
        }

        public string SavePath
        {
            get { return _savePath; }
            set
            {
                _savePath = value;
                NotifyOfPropertyChange(nameof(SavePath));
                NotifyOfPropertyChange(nameof(SavePathCorrect));
            }
        }

        public bool SavePathCorrect => FileManager.CheckFilePath(SavePath);

        public bool NotSaved
        {
            get { return _notSaved; }
            set
            {
                _notSaved = value;
                Debug.Debug.Instance.Post(
                    "Message",
                    "NotSaved",
                    new string[] { NotSaved.ToString() }
                );
                NotifyOfPropertyChange(nameof(NotSaved));
            }
        }

        #region Common Controls
        public ObservableCollection<WorkItem> CommonWork
        {
            get { return _commonWork; }
            set
            {
                _commonWork = value;
                NotifyOfPropertyChange(nameof(CommonWork));
            }
        }
        public ObservableCollection<WorkItem> FavoritedWork
        {
            get { return _favoritedWork; }
            set
            {
                _favoritedWork = value;
                NotifyOfPropertyChange(nameof(FavoritedWork));
            }
        }
        #endregion

        #region Calc Props
        public int TotalWork
        {
            get { return _totalWork; }
            set
            {
                _totalWork = value;
                NotifyOfPropertyChange(nameof(TotalWork));
            }
        }
        #endregion
        #endregion
    }
}
