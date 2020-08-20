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
            if (Debug.Debug.Instance.DefaultValues)
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
        public void HandleCellChanged( object sender, EventArgs e )
        {
            NotSaved = true;
        }

        public void HandleOpenSavePath( object sender, EventArgs e )
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                InitialDirectory = AppSettings.Default.DefaultSavePath,
                Title = "Save Location",
                CheckPathExists = false,
                CheckFileExists = false,
                OverwritePrompt = false,
                CreatePrompt = false,
                DefaultExt = FileManager.Extension,
                AddExtension = true,
                Filter = FileManager.FilterString
            };

            if (dialog.ShowDialog() == true)
            {
                SavePath = dialog.FileName;
            }
        }

        public void HandleOpenFile( object sender, EventArgs e )
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = AppSettings.Default.DefaultSavePath,
                Multiselect = false,
                DefaultExt = FileManager.Extension,
                AddExtension = true,
                Title = "Open Paystub File",
                Filter = FileManager.FilterString
            };

            if (dialog.ShowDialog() == true)
            {
                SavePath = dialog.FileName;
                OpenFile();
            }
            Debug.Debug.Instance.Post(
                "Message",
                "Open Dialog Result",
                new string[] { dialog.FileName }
            );
        }

        public void HandleSaveFile( object sender, EventArgs e )
        {
            try
            {
                FileManager.SaveFile(
                    SavePath,
                    PaystubCollection,
                    AppSettings.Default.OverwriteFile
                );
            }
            catch (Exception exe)
            {
                Debug.Debug.Instance.Post(
                    "Error",
                    exe.Message,
                    new string[] { exe.GetType().Name, SavePath, e.GetType().Name }
                );
            }
        }

        public void AddNewPaystubs( object sender, AddPaystubsEventArgs e )
        {
            if (e?.NewPaystubs.Count() > 0)
            {
                foreach (var paystub in e.NewPaystubs)
                {
                    PaystubCollection.Paystubs.Add(paystub);
                }
            }
        }

        /// <summary>
        /// Initializes a test PaystubCollection when Debug mode is active.
        /// </summary>
        private void BuildTempData( )
        {
            PaystubCollection = new PaystubCollection(AppSettings.Default.DefaultPayPeriod)
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
        }

        private bool CheckFileName( string path )
        {
            if (path?.Length > 0)
            {
                if (Directory.Exists(path))
                {
                    return true;
                }

                if (!File.Exists(path))
                {
                    var temp = Path.GetDirectoryName(path);
                    return Directory.Exists(temp);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void OpenFile( )
        {
            try
            {
                if (NotSaved)
                {
                    var result = MessageBox.Show(
                        "Somethings not saved. Open anyway?",
                        "Careful!",
                        MessageBoxButton.OKCancel
                    );
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                }
                PaystubCollection = FileManager.OpenFile(SavePath);
                NotSaved = false;
            }
            catch (Exception e)
            {
                Debug.Debug.Instance.Post(
                    "Error",
                    e.Message,
                    new string[] { e.GetType().Name, SavePath }
                );
            }
        }
        #endregion

        #region - Full Properties
        public PaystubCollection PaystubCollection
        {
            get { return _paystubCollection; }
            set
            {
                _paystubCollection = value;
                NotSaved = true;
                NotifyOfPropertyChange(nameof(PaystubCollection));
            }
        }

        public PaystubModel SelectedPaystub
        {
            get { return _selectedPaystub; }
            set
            {
                _selectedPaystub = value;
                NotifyOfPropertyChange(nameof(SelectedPaystub));
            }
        }

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

        public bool SavePathCorrect
        {
            get
            {
                return CheckFileName(SavePath);
            }
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
        #endregion
    }
}
