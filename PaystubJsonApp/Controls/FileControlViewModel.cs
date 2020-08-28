using PaystubJsonApp.FileControl;
using PaystubJsonApp.Models.Work;
using PaystubJsonApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PaystubJsonApp.Controls
{
    public class FileControlViewModel : ViewModelBase
    {
        #region - Fields & Properties
        private string _savePath;

        private bool _overwriteFile = false;
        #endregion

        #region - Constructors
        public FileControlViewModel( ) { }
        #endregion

        #region - Methods

        #endregion

        #region - Full Properties

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
