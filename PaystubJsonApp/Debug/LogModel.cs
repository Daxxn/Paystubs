using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Debug
{
    public enum LogType
    {
        Error,
        Warning,
        Message,
        Event,
    }

    public struct LogModel
    {
        #region - Fields & Properties
        public LogType Type { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Data { get; set; }
        private DateTime InstanceTime { get; set; }
        #endregion

        #region - Constructors
        public LogModel( LogType type, string message, IEnumerable<string> data )
        {
            Type = type;
            Message = message;
            Data = data;
            InstanceTime = DateTime.Now;
        }
        #endregion

        #region - Methods
        public override string ToString( ) => $"{Type} - {Message}\n{BuildData()}\nTime: {InstanceTime.ToShortDateString()} - {InstanceTime.ToShortTimeString()}";

        private string BuildData( )
        {
            if ( Data is null )
            {
                return "\tData: N/A";
            }
            StringBuilder builder = new StringBuilder("\tData:");
            foreach ( string d in Data )
            {
                builder.AppendLine($"\t\tType: {d.GetType()} - Value: {d}");
            }
            return builder.ToString();
        }
        #endregion

        #region - Full Properties

        #endregion
    }
}
