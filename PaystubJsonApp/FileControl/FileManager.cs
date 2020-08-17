using PaystubJsonApp.Models;
using Newtonsoft.Json;

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
        public static string[] FileExtensions { get; set; } = new string[0];
        public static string FilterString { get; set; } = "";
        public static string Extension { get; set; } = AppSettings.Default.DefaultFileExtension;
        public static string Delimiter { get; private set; } = AppSettings.Default.Delimiter;
        #endregion

        #region - Methods
        public static PaystubCollection OpenFile( string path )
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    return JsonConvert.DeserializeObject<PaystubCollection>(reader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SaveFile( string path, PaystubCollection data, bool overwrite )
        {
            try
            {
                if (!overwrite && File.Exists(path))
                {
                    throw new Exception("File Exists.");
                }

                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(JsonConvert.SerializeObject(data));
                    writer.Flush();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void OnStartup( )
        {
            ParseExtensions();
            BuildFilterString();
        }

        private static void ParseExtensions( )
        {
            try
            {
                FileExtensions = AppSettings.Default.FileExtensions.Split(
                    new string[] { Delimiter },
                    StringSplitOptions.RemoveEmptyEntries
                );
            }
            catch (Exception e)
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
                for (int i = 0; i < FileExtensions.Length; i++)
                {
                    builer.Append($"*{FileExtensions[ i ]}{(i < FileExtensions.Length - 1 ? ";" : "")}");
                }
                FilterString = builer.ToString();
            }
            catch (Exception e)
            {
                Debug.Debug.Instance.Post(
                    "Error",
                    "Filter Build Error: ",
                    new string[] { e.Message, FileExtensions.Length.ToString() }
                );
                throw;
            }
        }
        #endregion

        #region - Full Properties

        #endregion
    }
}
