using Microsoft.Win32;

using PaystubJsonApp.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PaystubJsonApp.FileControl
{
    public class FileDialogController<TModel> where TModel : ModelBase
    {
        #region - Fields & Properties
        public TModel Data { get; private set; }
        #endregion

        #region - Constructors
        public FileDialogController( ) { }
        public FileDialogController( TModel data ) => Data = data;
        #endregion

        #region - Methods
        public void SaveFile( string savePath )
        {
            try
            {
                FileManager.SaveFile(
                    savePath,
                    Data
                );
            }
            catch ( Exception exe )
            {
                Debug.Debug.Instance.Post(
                    "Error",
                    $"Save File Error: {exe.Message}",
                    new string[] { exe.GetType().Name, savePath }
                );
            }
        }

        public string SaveFileDialog( )
        {
            string newPath = null;
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = AppSettings.Default.DefaultSavePath,
                Multiselect = false,
                DefaultExt = FileManager.Extension,
                AddExtension = true,
                CheckFileExists = false,
                CheckPathExists = false,
                Title = "Open Paystub File",
                Filter = FileManager.FilterString
            };

            if ( dialog.ShowDialog() == true )
            {
                newPath = dialog.FileName;
            }
            return newPath;
        }

        public (string, TModel) OpenFile( bool notSaved )
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

            string newPath = null;
            TModel newData = null;

            if ( dialog.ShowDialog() == true )
            {
                newPath = dialog.FileName;
                try
                {
                    if ( notSaved )
                    {
                        MessageBoxResult result = MessageBox.Show(
                            "Somethings not saved. Open anyway?",
                            "Careful!",
                            MessageBoxButton.OKCancel
                        );
                        if ( result != MessageBoxResult.Cancel )
                        {
                            newData = FileManager.OpenFile<TModel>(newPath);

                        }
                    }
                }
                catch ( Exception e )
                {
                    Debug.Debug.Instance.Post(
                        "Error",
                        e.Message,
                        new string[] { e.GetType().Name, newPath }
                    );
                }
            }
            Debug.Debug.Instance.Post(
                "Message",
                "Open Dialog Result",
                new string[] { dialog.FileName }
            );
            return (newPath, newData);
        }

        public static bool CheckFilePath( string path )
        {
            if ( path?.Length > 0 )
            {
                if ( Directory.Exists(path) )
                {
                    return true;
                }

                if ( !File.Exists(path) )
                {
                    string temp = Path.GetDirectoryName(path);
                    return Directory.Exists(temp);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region - Full Properties

        #endregion
    }
}
