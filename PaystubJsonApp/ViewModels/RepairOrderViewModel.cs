using PaystubJsonApp.FileControl;
using PaystubJsonApp.Models.Paystubs;
using PaystubJsonApp.Models.RepairOrders;
using PaystubJsonApp.Models.Work;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PaystubJsonApp.ViewModels
{
    public class RepairOrderViewModel : ViewModelBase
    {
        #region - Fields & Properties
        private RepairOrderCollection _repairOrderCollection;
        private WorkCollection _allWork = WorkCollection.Instance;

        private DateTime _newRODate;

        private bool _overwriteFile;
        private string _savePath;
        private bool NotSaved { get; set; }

        #region RO Number
        private int _roNumber;
        private bool _limitRONumber;
        private int _roNumberDigitCount;
        #endregion

        #region Calc
        private double _totalFlatrateTime;
        private TimeSpan _dateDelta;

        private RepairOrder _highestRO;
        #endregion
        #endregion

        #region - Constructors
        public RepairOrderViewModel( )
        {
            if ( Debug.Debug.Instance.DefaultValues )
            {
                BuildDebugInstanceData();
            }
            else
            {
                RepairOrderCollection = new RepairOrderCollection();
            }
            LimitRONumber = true;
            RONumberDigitCount = 6;
            RONumber = MinRONumber;
            NewRODate = DateTime.Now;
        }
        #endregion

        #region - Methods
        public void RONumberDigitSelect( object sender, RoutedEventArgs e )
        {
            var selectedItem = e.Source as MenuItem;
            RONumberDigitCount = int.Parse((string)selectedItem.Header);
        }

        public void AddRepairOrder( object sender, EventArgs e )
        {
            if ( !CheckROList(RONumber) )
            {
                RepairOrderCollection.RepairOrders.Add(new RepairOrder()
                {
                    RONumber = RONumber,
                    Date = NewRODate,
                });
                Calculate(null, null);
            }
        }

        public void RemoveWorkFromRepairOrder( WorkItem selectedItem, RepairOrder currentRO )
        {
            if ( currentRO != null )
            {
                currentRO.Work.RemoveWork(selectedItem);
            }
            Calculate(null, null);
        }

        public void AddWorkToRepairOrder( WorkItem selectedItem, RepairOrder currentRO )
        {
            if ( currentRO != null )
            {
                currentRO.Work.AddWork(selectedItem);
            }
            Calculate(null, null);
        }

        #region Date Buttons
        public void DateUpEvent( object sender, EventArgs e )
        {
            try
            {
                NewRODate = NewRODate.AddDays(1);
            }
            catch ( Exception exe )
            {
                MessageBox.Show(exe.Message);
            }
        }

        public void DateDownEvent( object sender, EventArgs e )
        {
            try
            {
                NewRODate = NewRODate.AddDays(-1);
            }
            catch ( Exception exe )
            {
                MessageBox.Show(exe.Message);
            }
        }
        #endregion

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

        public void Calculate( object sender, EventArgs e )
        {
            if ( RepairOrderCollection.RepairOrders != null && RepairOrderCollection.RepairOrders.Count > 0 )
            {
                TotalFlatrateTime = RepairOrderCollection.RepairOrders.Sum(ro => ro.TotalTime);
                var highestTime = RepairOrderCollection.RepairOrders.Max(ro => ro.TotalTime);
                HighestRO = RepairOrderCollection.RepairOrders.First(ro => ro.TotalTime == highestTime);
                CalcDateDelta();
            }
        }
        private void CalcDateDelta( )
        {
            var minDate = RepairOrderCollection.RepairOrders.Min(ro => ro.Date);
            var maxDate = RepairOrderCollection.RepairOrders.Max(ro => ro.Date);
            DateDelta = maxDate.Subtract(minDate);
        }

        public void OpenSavePath( object sender, EventArgs e ) =>
            SavePath = new FileDialogController<RepairOrderCollection>().
                SaveFileDialog(FileExtensionType.RepairOrder);

        private bool CheckROList( int roNumber )
        {
            bool output = false;

            if ( RepairOrderCollection.Contains(roNumber) )
            {
                var result = MessageBox.Show("RO already exists. Create New RO?", "", MessageBoxButton.YesNo);

                if ( result == MessageBoxResult.Yes )
                {
                    output = true;
                }
            }

            return output;
        }

        public int CheckRONumberDigitLimit( int input )
        {
            if ( MinRONumber > input )
            {
                return MinRONumber;
            }
            else if ( MaxRONumber < input )
            {
                return MaxRONumber;
            }
            return input;
        }

        private void BuildDebugInstanceData( )
        {
            WorkCollection.New(new WorkItem[]
            {
                new WorkItem
                {
                    WorkIdNumber=1,
                    Name="Work A",
                    Description="This is the first one.",
                    FlatRateTime=1,
                },
                new WorkItem
                {
                    WorkIdNumber=2,
                    Name="Work B",
                    Description="This is another one.",
                    FlatRateTime=4,
                },
                new WorkItem
                {
                    WorkIdNumber=42,
                    Name="Work A",
                    Description="This is the Meaning of Life!",
                    FlatRateTime=1,
                },
            });
            RepairOrderCollection = new RepairOrderCollection()
            {
                RepairOrders = new ObservableCollection<RepairOrder>
                {
                    new RepairOrder
                    {
                        RONumber = 111111,
                        Date = DateTime.Now,
                        Work = new WorkProvider(WorkCollection.Instance.Data)
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
        #endregion

        #region - Full Properties
        public DateTime NewRODate
        {
            get { return _newRODate; }
            set
            {
                _newRODate = value;
                NotifyOfPropertyChange(nameof(NewRODate));
            }
        }
        public RepairOrderCollection RepairOrderCollection
        {
            get => _repairOrderCollection;
            set
            {
                _repairOrderCollection = value;
                NotifyOfPropertyChange(nameof(RepairOrderCollection));
                NotSaved = true;
            }
        }

        public WorkCollection AllWork
        {
            get { return _allWork; }
            set
            {
                _allWork = value;
                NotifyOfPropertyChange(nameof(AllWork));
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
        public bool SavePathCorrect => FileManager.CheckFilePath(SavePath);

        #region RO Number
        public int RONumber
        {
            get { return _roNumber; }
            set
            {
                _roNumber = CheckRONumberDigitLimit(value);
                NotifyOfPropertyChange(nameof(RONumber));
            }
        }

        public bool LimitRONumber
        {
            get { return _limitRONumber; }
            set
            {
                _limitRONumber = value;
                NotifyOfPropertyChange(nameof(LimitRONumber));
            }
        }

        public int RONumberDigitCount
        {
            get { return _roNumberDigitCount; }
            set
            {
                _roNumberDigitCount = value;
                NotifyOfPropertyChange(nameof(RONumberDigitCount));
            }
        }

        public int MinRONumber
        {
            get
            {
                return (int)Math.Pow(10, RONumberDigitCount - 1);
            }
        }

        public int MaxRONumber
        {
            get
            {
                return (int)Math.Pow(10, RONumberDigitCount) - 1;
            }
        }
        #endregion

        #region Calc
        public double TotalFlatrateTime
        {
            get { return _totalFlatrateTime; }
            set
            {
                _totalFlatrateTime = value;
                NotifyOfPropertyChange(nameof(TotalFlatrateTime));
            }
        }

        public TimeSpan DateDelta
        {
            get { return _dateDelta; }
            set
            {
                _dateDelta = value;
                NotifyOfPropertyChange(nameof(DateDelta));
            }
        }

        public RepairOrder HighestRO
        {
            get { return _highestRO; }
            set
            {
                _highestRO = value;
                NotifyOfPropertyChange(nameof(HighestRO));
            }
        }
        #endregion
        #endregion
    }
}
