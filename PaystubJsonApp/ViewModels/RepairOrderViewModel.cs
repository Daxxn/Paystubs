using PaystubJsonApp.FileControl;
using PaystubJsonApp.Models.Paystubs;
using PaystubJsonApp.Models.ReapirOrders;
using PaystubJsonApp.Models.RepairOrders;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PaystubJsonApp.ViewModels
{
    public class RepairOrderViewModel : ViewModelBase
    {
        #region - Fields & Properties
        private RepairOrderCollection _repairOrderCollection;
        private RepairOrder _newRepairOrder;

        private bool _overwriteFile;
        private string _savePath;
        private bool _savePathCorrect;
        private bool NotSaved { get; set; }
        #endregion

        #region - Constructors
        public RepairOrderViewModel( )
        {
            NewRepairOrder = new RepairOrder();
            BuildDebugInstanceData();
        }
        #endregion

        #region - Methods
        public void AddRepairOrder( object sender, EventArgs e )
        {
            RepairOrderCollection.RepairOrders.Add(new RepairOrder()
            {
                RONumber = 111,
                Date = new DateTime(2020, 5, 5)
            });
            ;
        }

        public void RemoveWorkFromRepairOrder( WorkItem item, RepairOrder currentRO )
        {
            if ( currentRO != null )
            {
                if ( item != null )
                {
                    currentRO.Work.RemoveWorkItem(item, false);
                }
            }
        }

        public void AddWorkToRepairOrder( WorkItem selectedItem, RepairOrder currentRO )
        {
            if ( currentRO != null )
            {
                if ( !currentRO.Work.WorkData.Contains(selectedItem) )
                {
                    currentRO.Work.AddWorkItem(selectedItem);
                }
            }
        }

        public void SaveFile( object sender, EventArgs e )
        {
            FileDialogController<RepairOrderCollection> saver = new FileDialogController<RepairOrderCollection>(RepairOrderCollection);
            saver.SaveFile(SavePath);
        }

        public void OpenFile( object sender, EventArgs e )
        {
            FileDialogController<RepairOrderCollection> opener = new FileDialogController<RepairOrderCollection>();
            (string newPath, RepairOrderCollection newROs) = opener.OpenFile(NotSaved);
            if ( newPath != null && newROs != null )
            {
                SavePath = newPath;
                RepairOrderCollection = newROs;
            }
            NotSaved = false;
        }

        public void OpenSavePath( object sender, EventArgs e ) => SavePath = new FileDialogController<RepairOrderCollection>().SaveFileDialog();

        private void BuildDebugInstanceData( )
        {
            if ( Debug.Debug.Instance.DefaultValues )
            {
                RepairOrderCollection = new RepairOrderCollection()
                {
                    RepairOrders = new ObservableCollection<RepairOrder>
                    {
                        new RepairOrder
                        {
                            RONumber = 111111,
                            Date = DateTime.Now,
                            Work = new WorkProvider()
                        },
                        new RepairOrder
                        {
                            RONumber = 222222,
                            Date = DateTime.Now,
                            Work = new WorkProvider()
                        }
                    }
                };
            }
        }
        #endregion

        #region - Full Properties
        public RepairOrderCollection RepairOrderCollection
        {
            get => _repairOrderCollection;
            set
            {
                _repairOrderCollection = value;
                NotifyOfPropertyChange(nameof(RepairOrderCollection));
                NotifyOfPropertyChange(nameof(NoROs));
                NotSaved = true;
            }
        }

        public RepairOrder NewRepairOrder
        {
            get => _newRepairOrder;
            set
            {
                _newRepairOrder = value;
                NotifyOfPropertyChange(nameof(NewRepairOrder));
            }
        }

        public Visibility NoROs
        {
            get
            {
                if ( RepairOrderCollection != null )
                {
                    if ( RepairOrderCollection.RepairOrders != null )
                    {
                        return RepairOrderCollection.RepairOrders.Count <= 0 ? Visibility.Visible : Visibility.Hidden;
                    }
                }
                return Visibility.Visible;
            }
        }

        public bool OverwriteFile
        {
            get => _overwriteFile;
            set
            {
                _overwriteFile = value;
                NotifyOfPropertyChange(nameof(OverwriteFile));
            }
        }

        public string SavePath
        {
            get => _savePath;
            set
            {
                _savePath = value;
                NotifyOfPropertyChange(nameof(SavePath));
                NotifyOfPropertyChange(nameof(SavePathCorrect));
            }
        }

        public bool SavePathCorrect
        {
            get => _savePathCorrect;
            set
            {
                _savePathCorrect = value;
                NotifyOfPropertyChange(nameof(SavePathCorrect));
            }
        }
        #endregion
    }
}
