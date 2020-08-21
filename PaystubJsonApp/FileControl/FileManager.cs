using Newtonsoft.Json;

using PaystubJsonApp.Models;
using PaystubJsonApp.Models.Paystubs;
using PaystubJsonApp.Models.RepairOrders;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.FileControl
{
    public static class FileManager
    {
        #region - Fields & Properties
        public static string[] PaystubFileExtensions { get; set; } = new string[ 0 ];
        public static string[] RepairOrderFileExtensions { get; set; } = new string[ 0 ];
        public static string[] WorkOrderFileExtensions { get; set; } = new string[ 0 ];
        public static string FilterString { get; set; } = "";
        public static string Extension { get; set; } = AppSettings.Default.DefaultFileExtension;
        public static string Delimiter { get; private set; } = AppSettings.Default.Delimiter;
        #endregion

        #region - Methods

        public static TModel OpenFile<TModel>( string path ) where TModel : ModelBase
        {
            try
            {
                using ( StreamReader reader = new StreamReader(path) )
                {
                    return JsonConvert.DeserializeObject<TModel>(reader.ReadToEnd());
                }
            }
            catch ( Exception )
            {
                throw;
            }
        }

        public static void SaveFile<TModel>( string path, TModel data )
        {
            try
            {
                using ( StreamWriter writer = new StreamWriter(path) )
                {
                    writer.Write(JsonConvert.SerializeObject(data));
                    writer.Flush();
                }
            }
            catch ( Exception )
            {
                throw;
            }
        }

        public static void OnStartup( )
        {
            PaystubFileExtensions = ParseExtensions(
                AppSettings.Default.PaystubFileExtensions
            );
            RepairOrderFileExtensions = ParseExtensions(
                AppSettings.Default.RepairOrderFileExtensions
            );
            WorkOrderFileExtensions = ParseExtensions(
                AppSettings.Default.WorkOrderFileExtensions
            );
            BuildFilterString();
        }

        private static string[] ParseExtensions( string extensions )
        {
            try
            {
                return extensions.Split(
                    new string[] { Delimiter },
                    StringSplitOptions.RemoveEmptyEntries
                );
            }
            catch ( Exception e )
            {
                Debug.Debug.Instance.Post(
                    "Error",
                    $"Extension Parse Error: ",
                    new string[] { e.Message, Delimiter }
                );
                throw e;
            }
        }

        public static void BuildFilterString( )
        {
            try
            {
                StringBuilder builer = new StringBuilder("Paystub File|");
                for ( int i = 0; i < PaystubFileExtensions.Length; i++ )
                {
                    builer.Append($"*{PaystubFileExtensions[ i ]}{( i < PaystubFileExtensions.Length - 1 ? ";" : "" )}");
                }
                FilterString = builer.ToString();
            }
            catch ( Exception e )
            {
                Debug.Debug.Instance.Post(
                    "Error",
                    "Filter Build Error: ",
                    new string[] { e.Message, PaystubFileExtensions.Length.ToString() }
                );
                throw;
            }
        }
        #endregion

        #region - Full Properties

        #endregion
    }
}
