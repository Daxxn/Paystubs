using Microsoft.Win32;

using PaystubJsonApp.FileControl;
using PaystubJsonApp.Logic;
using PaystubJsonApp.Models.Paystubs;
using PaystubJsonApp.ViewModels.Events;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PaystubJsonApp.ViewModels
{
    enum CalcType
    {
        Average,
        Sum
    }

    public enum Prop
    {
        Gross,
        Net,
        Hours,
        Flat
    }

    public class PaystubViewModel : ViewModelBase
    {
        #region - Fields & Properties
        private PaystubCollection _paystubCollection;
        private PaystubModel _selectedPaystub;

        private PaystubModel _selectedFilterPaystub;

        private Dictionary<string, Func<PaystubCollection, Prop, PaystubModel>> _filterSelections;
        private string _selectedFilter = "Max";
        private Prop _selectedProp;

        private bool _notSaved;
        private bool _overwriteFile;
        private string _savePath;

        public decimal _averageGross;
        public decimal _averageNet;
        public double _averageHours;
        private double _averageFlatrate;

        private decimal _grossSum;
        private decimal _netSum;
        private double _hoursSum;
        private double _flatrateSum;
        #endregion

        #region - Constructors
        public PaystubViewModel( )
        {
            OverwriteFile = AppSettings.Default.OverwriteFile;
            NotSaved = false;

            FilterSelections = FilterMethods.PaystubFilters;

            if ( Debug.Debug.Instance.DefaultValues )
            {
                BuildTempData();
            }
            else
            {
                PaystubCollection = new PaystubCollection(
                    AppSettings.Default.DefaultPayPeriod
                );
            }
        }
        #endregion

        #region - Methods
        public void HandleCellChanged( object sender, EventArgs e ) => NotSaved = true;

        public void HandleOpenSavePath( object sender, EventArgs e ) =>
            SavePath = new FileDialogController<PaystubCollection>().
                SaveFileDialog(FileExtensionType.Paystub);
        public void HandleOpenFile( object sender, EventArgs e )
        {
            FileDialogController<PaystubCollection> opener = new FileDialogController<PaystubCollection>();
            (string newPath, PaystubCollection newPaystubs) = opener.OpenFile(NotSaved);
            if ( newPath != null && newPaystubs != null )
            {
                SavePath = newPath;
                PaystubCollection = newPaystubs;
            }
            NotSaved = false;
        }

        public void HandleSaveFile( object sender, EventArgs e )
        {
            FileDialogController<PaystubCollection> saver = new FileDialogController<PaystubCollection>(PaystubCollection);
            saver.SaveFile(SavePath);
        }

        public void AddNewPaystubs( object sender, AddPaystubsEventArgs e )
        {
            if ( e?.NewPaystubs.Count() > 0 )
            {
                foreach ( PaystubModel paystub in e.NewPaystubs )
                {
                    PaystubCollection.Paystubs.Add(paystub);
                }
                Calculate(null, null);
            }
        }

        #region Calculation Methods
        public void Calculate( object sender, EventArgs e )
        {
            Calc(CalcType.Average, Prop.Gross);
            Calc(CalcType.Average, Prop.Net);
            Calc(CalcType.Average, Prop.Hours);
            Calc(CalcType.Average, Prop.Flat);

            Calc(CalcType.Sum, Prop.Gross);
            Calc(CalcType.Sum, Prop.Net);
            Calc(CalcType.Sum, Prop.Hours);
            Calc(CalcType.Sum, Prop.Flat);

            UpdateFilter();
        }

        private void Calc( CalcType type, Prop propName )
        {
            if ( type == CalcType.Average )
            {
                switch ( propName )
                {
                    case Prop.Gross:
                        AverageGross = PaystubCollection.Paystubs.Average(x => x.Gross);
                        break;
                    case Prop.Net:
                        AverageNet = PaystubCollection.Paystubs.Average(x => x.Net);
                        break;
                    case Prop.Hours:
                        AverageHours = PaystubCollection.Paystubs.Average(x => x.Hours);
                        break;
                    case Prop.Flat:
                        AverageFlatrate = PaystubCollection.Paystubs.Average(x => x.FlatrateHours);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch ( propName )
                {
                    case Prop.Gross:
                        GrossSum = PaystubCollection.Paystubs.Sum(x => x.Gross);
                        break;
                    case Prop.Net:
                        NetSum = PaystubCollection.Paystubs.Sum(x => x.Net);
                        break;
                    case Prop.Hours:
                        HoursSum = PaystubCollection.Paystubs.Sum(x => x.Hours);
                        break;
                    case Prop.Flat:
                        FlatrateSum = PaystubCollection.Paystubs.Sum(x => x.FlatrateHours);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Filter Methods
        public void FilterSelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if ( e.AddedItems.Count > 0 )
            {
                SelectedFilterPaystub = FilterSelections[ ( string )e.AddedItems[ 0 ] ](PaystubCollection, SelectedProp);
                SelectedFilter = (string)e.AddedItems[ 0 ];
            }
        }

        public void PropSelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if ( e.AddedItems.Count > 0 )
            {
                SelectedFilterPaystub = FilterSelections[ SelectedFilter ](PaystubCollection, ( Prop )e.AddedItems[ 0 ]);
                SelectedProp = ( Prop )e.AddedItems[ 0 ];
            }
        }

        private void UpdateFilter( )
        {
            if ( SelectedFilter != null )
            {
                SelectedFilterPaystub = FilterSelections[ SelectedFilter ](PaystubCollection, SelectedProp);
            }
        }
        #endregion

        /// <summary>
        /// Initializes a test PaystubCollection when Debug mode is active.
        /// </summary>
        private void BuildTempData( ) => PaystubCollection = new PaystubCollection(AppSettings.Default.DefaultPayPeriod)
        {
            Paystubs = new ObservableCollection<PaystubModel>
                {
                    new PaystubModel
                    {
                        Gross = 100,
                        Net = 50,
                        Hours = 40,
                        FlatrateHours = 50,
                        Rate = 2,
                        StartDate = new DateTime(2020, 1, 1),
                        EndDate = new DateTime(2020, 1, 14)
                    }
                }
        };
        #endregion

        #region - Full Properties
        public PaystubCollection PaystubCollection
        {
            get => _paystubCollection;
            set
            {
                _paystubCollection = value;
                NotSaved = true;
                NotifyOfPropertyChange(nameof(PaystubCollection));
                NotifyOfPropertyChange(nameof(AverageGross));
            }
        }

        public PaystubModel SelectedPaystub
        {
            get => _selectedPaystub;
            set
            {
                _selectedPaystub = value;
                NotifyOfPropertyChange(nameof(SelectedPaystub));
            }
        }

        public PaystubModel SelectedFilterPaystub
        {
            get { return _selectedFilterPaystub; }
            set
            {
                _selectedFilterPaystub = value;
                NotifyOfPropertyChange(nameof(SelectedFilterPaystub));
            }
        }

        public Dictionary<string, Func<PaystubCollection, Prop, PaystubModel>> FilterSelections
        {
            get { return _filterSelections; }
            set
            {
                _filterSelections = value;
                NotifyOfPropertyChange(nameof(FilterSelections));
            }
        }

        public string SelectedFilter
        {
            get { return _selectedFilter; }
            set
            {
                _selectedFilter = value;
                NotifyOfPropertyChange(nameof(SelectedFilter));
                NotifyOfPropertyChange(nameof(FilterPaystubTitle));
            }
        }

        public Prop SelectedProp
        {
            get { return _selectedProp; }
            set
            {
                _selectedProp = value;
                NotifyOfPropertyChange(nameof(SelectedProp));
            }
        }

        public bool NotSaved
        {
            get => _notSaved;
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

        public bool OverwriteFile
        {
            get => _overwriteFile;
            set
            {
                _overwriteFile = value;
                NotifyOfPropertyChange(nameof(OverwriteFile));
            }
        }

        public string FilterPaystubTitle
        {
            get
            {
                return SelectedFilterPaystub is null ? "Paystub" : $"{SelectedFilter} Paystub";
            }
        }

        #region Calculations
        #region Averages
        public decimal AverageGross
        {
            get { return _averageGross; }
            set
            {
                _averageGross = value;
                NotifyOfPropertyChange(nameof(AverageGross));
            }
        }

        public decimal AverageNet
        {
            get { return _averageNet; }
            set
            {
                _averageNet = value;
                NotifyOfPropertyChange(nameof(AverageNet));
            }
        }

        public double AverageHours
        {
            get { return _averageHours; }
            set
            {
                _averageHours = value;
                NotifyOfPropertyChange(nameof(AverageHours));
            }
        }


        public double AverageFlatrate
        {
            get { return _averageFlatrate; }
            set
            {
                _averageFlatrate = value;
                NotifyOfPropertyChange(nameof(AverageFlatrate));
            }
        }
        #endregion

        #region Sums
        public decimal GrossSum
        {
            get { return _grossSum; }
            set
            {
                _grossSum = value;
                NotifyOfPropertyChange(nameof(GrossSum));
            }
        }

        public decimal NetSum
        {
            get { return _netSum; }
            set
            {
                _netSum = value;
                NotifyOfPropertyChange(nameof(NetSum));
            }
        }

        public double HoursSum
        {
            get { return _hoursSum; }
            set
            {
                _hoursSum = value;
                NotifyOfPropertyChange(nameof(HoursSum));
            }
        }
        public double FlatrateSum
        {
            get { return _flatrateSum; }
            set
            {
                _flatrateSum = value;
                NotifyOfPropertyChange(nameof(FlatrateSum));
            }
        }
        #endregion
        #endregion
        #endregion
    }
}
