using Microsoft.Win32;

using PaystubJsonApp.FileControl;
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

namespace PaystubJsonApp.ViewModels
{
    public class PaystubViewModel : ViewModelBase
    {
        #region - Fields & Properties
        private PaystubCollection _paystubCollection;
        private PaystubModel _selectedPaystub;

        private bool _notSaved;
        private bool _overwriteFile;
        private string _savePath;
        #endregion

        #region - Constructors
        public PaystubViewModel( )
        {
            OverwriteFile = AppSettings.Default.OverwriteFile;
            NotSaved = false;
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
            }
        }

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
        #endregion
    }
}
