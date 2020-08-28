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
    public enum FileExtensionType
    {
        Paystub,
        RepairOrder,
        WorkOrder
    }
    public static class FileManager
    {
        #region - Fields & Properties
        private static readonly string AllFilesFilter = "| All Files | *.*";
        public static string[] PaystubFileExtensions { get; set; } = new string[ 0 ];
        public static string[] RepairOrderFileExtensions { get; set; } = new string[ 0 ];
        public static string[] WorkOrderFileExtensions { get; set; } = new string[ 0 ];
        public static Dictionary<FileExtensionType, string> Filters { get; set; }
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
            //BuildFilterString();
            BuildFilterStrings();
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

        //public static void BuildFilterString( )
        //{
        //    try
        //    {
        //        StringBuilder builer = new StringBuilder("Paystub File|");
        //        for ( int i = 0; i < PaystubFileExtensions.Length; i++ )
        //        {
        //            builer.Append($"*{PaystubFileExtensions[ i ]}{( i < PaystubFileExtensions.Length - 1 ? ";" : "" )}");
        //        }
        //        FilterString = builer.ToString();
        //    }
        //    catch ( Exception e )
        //    {
        //        Debug.Debug.Instance.Post(
        //            "Error",
        //            "Filter Build Error: ",
        //            new string[] { e.Message, PaystubFileExtensions.Length.ToString() }
        //        );
        //        throw;
        //    }
        //}

        //public static void BuildFilterString( string extentionType )
        //{
        //    try
        //    {
        //        StringBuilder builder = new StringBuilder($"{extentionType} File |");
        //        Action<string[]> buildFunc = ( string[] extensions ) =>
        //        {
        //            for ( int i = 0; i < extensions.Length; i++ )
        //            {
        //                builder.Append($"*{extensions[ i ]}{( i < extensions.Length - 1 ? ";" : "" )}");
        //            }
        //        };
        //        extentionType = extentionType.Trim();
        //        extentionType.Replace(' ', '\0');
        //        switch ( extentionType )
        //        {
        //            case "Paystub":
        //                buildFunc(PaystubFileExtensions);
        //                break;
        //            case "RepairOrder":
        //                buildFunc(RepairOrderFileExtensions);
        //                break;
        //            case "WorkOrder":
        //                buildFunc(WorkOrderFileExtensions);
        //                break;
        //            default:
        //                throw new ArgumentException($"Extension Type doesnt match any known types: {extentionType}");
        //        }
        //    }
        //    catch ( Exception e )
        //    {
        //        Debug.Debug.Instance.Post(
        //            "Error",
        //            "Filter Build Error: ",
        //            new string[] { e.Message, PaystubFileExtensions.Length.ToString() }
        //        );
        //        throw;
        //    }
        //}

        public static void BuildFilterStrings( )
        {
            Func<FileExtensionType, string[], string> buildFunc = ( FileExtensionType type, string[] extensions ) =>
            {
                StringBuilder builder = new StringBuilder($"{type} File |");
                for ( int i = 0; i < extensions.Length; i++ )
                {
                    builder.Append($"*{extensions[ i ]}{( i < extensions.Length - 1 ? ";" : "" )}");
                }
                builder.Append(AllFilesFilter);
                return builder.ToString();
            };
            Filters = new Dictionary<FileExtensionType, string>();

            Filters.Add(
                FileExtensionType.Paystub,
                buildFunc(
                    FileExtensionType.Paystub,
                    PaystubFileExtensions
                )
            );

            Filters.Add(
                FileExtensionType.RepairOrder,
                buildFunc(
                    FileExtensionType.RepairOrder,
                    RepairOrderFileExtensions
                )
            );
            
            Filters.Add(
                FileExtensionType.WorkOrder,
                buildFunc(
                    FileExtensionType.WorkOrder,
                    WorkOrderFileExtensions
                )
            );
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
